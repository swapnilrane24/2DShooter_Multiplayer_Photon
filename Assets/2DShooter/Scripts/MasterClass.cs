using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiplayerSystem;
using UISystem;
using Commons;

namespace MasterClass
{
    public class MasterClass : Singleton<MasterClass>
    {
        [SerializeField]
        private UIView uIView;

        IMultiplayer multiplayer;
        IUIService uiService;

        private void Start()
        {
            multiplayer = new MultiplayerService();
            uiService = new UIService(uIView);
            uiService.SetMultiplayerService(multiplayer);
            multiplayer.SetUIService(uiService);

            multiplayer.ConnectToServer();
        }
    }
}