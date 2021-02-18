using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class StoreItem : TrasferItemCommand
//{

//        Character giver;
//        int _index;
//        public StoreItem(Character curentCharacter, int index) : this(curentCharacter.TilePawnIsOn, (iCanGiveItems)curentCharacter, index)
//        {

//        }

//        public StoreItem(Tile starTile, iCanGiveItems curentCharacter, int index) : base(index)
//        {
//            if (curentCharacter is Character)
//            {
//                this._index = index;
//                _giver = (iCanGiveItems)curentCharacter;
//                giver = (Character)_giver;
//                isUsable = giver.cariedObjects.Count > 0;
//                typeOfCommand = new HighlightTilesCommand(1, starTile, OrganizeTrade, EnumHolder.EntityType.Container);
//            }
//        }

//        public override string CommandName
//        {
//            get
//            {
//                return "StoreItme";
//            }
//        }

//        protected override void OrganizeTrade(Tile tileToTradeWith)
//        {
//          iCaryable swapedItem = _giver.Give(_index);
//            _giver.GetRidOfItem(_index);
//           //tileToTradeWith.TargetableOnTile.cariedObjects.add();// Reciver.PickUp(swapedItem);  // TODO For next time. We need to see about picking up and putting down items for
//            typeOfCommand.UndoType();                        // Things other than characters. Then we want to factor this out in a way where cooking stations inherit from abstract storage units.  Then work on a trade gui
//            Reciver.characterCoaster.SetArtForFacing(Reciver.characterCoaster.determineFacing(Reciver.TilePawnIsOn, tileToTradeWith));
//    }

//        public override bool isUsable { get; set; }
//        public override iCommandKind typeOfCommand { get; set; }
//    }

