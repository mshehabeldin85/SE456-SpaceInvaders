using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  Singleton class - use only public static methods for customers
    //
    //  * One single compare node is owned by this singleton - used for find, prevent extra news
    //  * Create one - NULL Object - Image Default
    //  * Dependency - TextureMan needs to be initialized before ImageMan
    //
    //---------------------------------------------------------------------------------------------------------
    public class TimerMan : Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public TimerMan(int reserveNum = 3, int reserveGrow = 1)
        : base()
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new TimeEvent();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        //public static void Create(int reserveNum = 3, int reserveGrow = 1)
        //{
        //    // make sure values are ressonable 
        //    Debug.Assert(reserveNum > 0);
        //    Debug.Assert(reserveGrow > 0);

        //    // initialize the singleton here
        //    Debug.Assert(pInstance == null);

        //    // Do the initialization
        //    if (pInstance == null)
        //    {
        //        pInstance = new TimerMan(reserveNum, reserveGrow);
        //    }
        //}

        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TimerMan();
            }
        }

        public static void Destroy()
        {
            //TimerMan pMan = TimerMan.privGetInstance();
            TimerMan pMan = TimerMan.pActiveTmMan;
            Debug.Assert(pMan != null);

            // Print stats on destroy
            pMan.baseDump();

            // Invalidate the singleton
            if (pInstance != null)
            {
                pInstance = null;
            }
        }

        public static TimeEvent Add(TimeEvent.Name timeName, Command pCommand, float deltaTimeToTrigger)
        {
            //TimerMan pMan = TimerMan.privGetInstance();
            TimerMan pMan = TimerMan.pActiveTmMan;
            Debug.Assert(pMan != null);

            TimeEvent pNode = TimerMan.privSortedAdd(deltaTimeToTrigger);
            Debug.Assert(pNode != null);

            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger >= 0.0f);

            pNode.Set(timeName, pCommand, deltaTimeToTrigger);
            return pNode;
        }

        public static TimeEvent Find(TimeEvent.Name name)
        {
            //TimerMan pMan = TimerMan.privGetInstance();
            TimerMan pMan = TimerMan.pActiveTmMan;
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            Debug.Assert(pMan.poNodeCompare != null);
            pMan.poNodeCompare.Wash();
            pMan.poNodeCompare.name = name;

            TimeEvent pData = (TimeEvent)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(TimeEvent pNode)
        {
            //TimerMan pMan = TimerMan.privGetInstance();
            TimerMan pMan = TimerMan.pActiveTmMan;
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.baseRemove(pNode);
        }

        public static void Dump()
        {
            //TimerMan pMan = TimerMan.privGetInstance();
            TimerMan pMan = TimerMan.pActiveTmMan;
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        public static void PauseUpdate(float delta)
        {
            // Get the instance
            //TimerMan pMan = TimerMan.privGetInstance();
            TimerMan pMan = TimerMan.pActiveTmMan;
            Debug.Assert(pMan != null);

            // walk the list
            TimeEvent pEvent = (TimeEvent)pMan.baseGetActive();

            while (pEvent != null)
            {
                pEvent.triggerTime += delta;
                // advance the pointer
                pEvent = (TimeEvent)pEvent.pNext;
            }
        }

        public static void Update(float totalTime)
        {
            // Get the instance
            //TimerMan pMan = TimerMan.privGetInstance();
            TimerMan pMan = TimerMan.pActiveTmMan;
            Debug.Assert(pMan != null);

            // squirrel away
            pMan.mCurrTime = totalTime;

            // walk the list
            TimeEvent pEvent = (TimeEvent)pMan.baseGetActive();
            TimeEvent pNextEvent = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent 
            // ToDo Fix: List needs to be sorted
            while (pEvent != null)
            {
                // Difficult to walk a list and remove itself from the list
                // so squirrel away the next event now, use it at bottom of while
                pNextEvent = (TimeEvent)pEvent.pNext;
                
                if (pMan.mCurrTime >= pEvent.triggerTime)
                {

                    // call it
                    pEvent.Process();

                    // remove from list
                    pMan.baseRemove(pEvent);
                }

                // advance the pointer
                pEvent = pNextEvent;
            }
        }

        public static float GetCurrTime()
        {
            // Get the instance
            //TimerMan pTimerMan = TimerMan.privGetInstance();
            TimerMan pMan = TimerMan.pActiveTmMan;

            // return time
            return pMan.mCurrTime;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink derivedCreateNode()
        {
            DLink pNode = new TimeEvent();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected Boolean derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            TimeEvent pDataA = (TimeEvent)pLinkA;
            TimeEvent pDataB = (TimeEvent)pLinkB;

            Boolean status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }

        override protected void derivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimeEvent pNode = (TimeEvent)pLink;
            pNode.Wash();
        }

        override protected void derivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimeEvent pData = (TimeEvent)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TimerMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        private static TimeEvent privSortedAdd(float deltaTimeToTrigger)
        {
            DLink pLink;
            TimeEvent pCur;
            TimeEvent pTmp;

            //TimerMan pMan = TimerMan.privGetInstance();
            TimerMan pMan = TimerMan.pActiveTmMan;
            Debug.Assert(pMan != null);

            TimeEvent pTimeEvent = (TimeEvent)pMan.baseGetActive();
            TimeEvent pNode = (TimeEvent)pMan.pullNodeFromReserve();

            float newTriggerTime = TimerMan.GetCurrTime() + deltaTimeToTrigger;

            if (pTimeEvent == null || pTimeEvent.triggerTime >= newTriggerTime)
            {
                pNode = (TimeEvent)pMan.baseAddEnd();
                Debug.Assert(pNode != null);
            }
            else
            {
                Debug.Assert(pNode != null);

                pCur = pTimeEvent;

                while (pCur.pNext != null)
                {
                    pTmp = (TimeEvent)pCur.pNext;

                    if (pTmp.triggerTime <= newTriggerTime)
                    {
                        pCur = (TimeEvent)pCur.pNext;
                        break;
                    }
                }

                pLink = (DLink)pCur;
                pNode = (TimeEvent)pMan.baseAddAfter(ref pLink, pNode);
                Debug.Assert(pNode != null);
            }

            return pNode;
        }

        public static void SetActive(TimerMan pTmMan)
        {
            TimerMan pMan = TimerMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pTmMan != null);
            TimerMan.pActiveTmMan = pTmMan;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static TimerMan pInstance = null;
        private TimeEvent poNodeCompare;
        protected float mCurrTime;
        private static TimerMan pActiveTmMan = null;
    }
}

// End of File
