using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EggNamespace.UI.View
{
    public class SimpleViewController : ViewControllerBase
    {
        public override void OpenView()
        {
            base.OpenView();
            this.gameObject.SetActive(true);
        }
        public override void CloseView()
        {
            base.CloseView();
            this.gameObject.SetActive(false);
        }
    }
}