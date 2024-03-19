using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Controllers.Menu{
    public abstract class Menuitem
    {
        // Start is called before the first frame update
        public Menuitem(){}

        public abstract void OnMouseUp();
    }
}
