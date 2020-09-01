using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InputMan
    {
        public InputMan()
        {
            this.pSubjectArrowLeft = new InputSubject();
            this.pSubjectArrowRight = new InputSubject();
            this.pSubjectSpace = new InputSubject();
            this.pSubjectKey_B = new InputSubject();
            this.pSubjectKey_1 = new InputSubject();
            this.pSubjectKey_2 = new InputSubject();
            this.pSubjectKey_Q = new InputSubject();

            this.privSpaceKeyPrev = false;
        }

        public static void SetActive(InputMan pInputMan)
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pInputMan != null);
            InputMan.pInstance = pInputMan;
        }

        private static InputMan privGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new InputMan();
            }
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        public static InputSubject GetArrowRightSubject()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowRight;
        }

        public static InputSubject GetArrowLeftSubject()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowLeft;
        }

        public static InputSubject GetSpaceSubject()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectSpace;
        }

        public static InputSubject GetKey_B_Subject()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectKey_B;
        }

        public static InputSubject GetKey_1_Subject()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectKey_1;
        }

        public static InputSubject GetKey_2_Subject()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectKey_2;
        }

        public static InputSubject GetKey_Q_Subject()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectKey_Q;
        }

        public static void Update()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            bool spaceKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);

            if (spaceKeyCurr == true && pMan.privSpaceKeyPrev == false)
            {
                pMan.pSubjectSpace.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                pMan.pSubjectArrowLeft.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                pMan.pSubjectArrowRight.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_B) == true)
            {
                pMan.pSubjectKey_B.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1) == true)
            {
                pMan.pSubjectKey_1.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2) == true)
            {
                pMan.pSubjectKey_2.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Q) == true)
            {
                pMan.pSubjectKey_Q.Notify();
            }

            pMan.privSpaceKeyPrev = spaceKeyCurr;

        }

        // Data: ----------------------------------------------
        private static InputMan pInstance = null;
        private bool privSpaceKeyPrev;

        private InputSubject pSubjectArrowRight;
        private InputSubject pSubjectArrowLeft;
        private InputSubject pSubjectSpace;
        private InputSubject pSubjectKey_B;
        private InputSubject pSubjectKey_1;
        private InputSubject pSubjectKey_2;
        private InputSubject pSubjectKey_Q;
    }
}
