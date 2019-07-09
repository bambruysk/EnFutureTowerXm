using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EnFutureTowerXm
{
    public enum PostType { ATTACK1, ATTACK2,DEFENCE };

    public class ArtefactListView
    {
        public TextView TextViewArtefactTop;
        public TextView TextViewArtefactMid;
        public TextView TextViewArtefactLow;

        public RadioGroup radioGroup;

        public List<string> ListAllArtefacts;

        public List<string> artefacts;

        public ArtefactListView (Activity context)
        {
            TextViewArtefactTop = context.FindViewById<TextView>(Resource.Id.textViewArtefactTop);
            TextViewArtefactMid = context.FindViewById<TextView>(Resource.Id.textViewArtefactMid);
            TextViewArtefactLow = context.FindViewById<TextView>(Resource.Id.textViewArtefactLow);

            ListAllArtefacts = new List<string> { { "Бомба" }, { "Аптечка" }, { "Регенерация" }, { "Яд" }, { "Иммунитет" } };

            var rand = new Random();
            artefacts = new List<string>();

            radioGroup = context.FindViewById<RadioGroup>(Resource.Id.radioGroup1);

            Show();
        }



        public void Show()
        {
            switch (radioGroup.CheckedRadioButtonId)
            {
                case Resource.Id.radioButtonAttack1:
                    artefacts = new List<string> { { "Бомба" }, { "Яд" }, { "Яд" } };
                    break;
                case Resource.Id.radioButtonAttack2:
                    artefacts = new List<string> { { "Бомба" }, { "Яд" }, { "Бомба" } };
                    break;
                case Resource.Id.radioButtonDefence:
                    artefacts = new List<string> { { "Аптечка" }, { "Регенерация" }, { "Иммунитет" } };
                    break;
            }
            artefacts = Shuffle( artefacts);
            UpdateView();
        }

        public void UpdateView()
        {
            TextViewArtefactTop.Text = artefacts[0];
            TextViewArtefactMid.Text = artefacts[1];
            TextViewArtefactLow.Text = artefacts[2];
        }

        public void Next1()
        {
            TextViewArtefactTop.Text = artefacts[1];
            TextViewArtefactMid.Text = artefacts[2];
            TextViewArtefactLow.Text = "";
        }

        public void Next2()
        {
            TextViewArtefactTop.Text = artefacts[2];
            TextViewArtefactMid.Text = "";
            TextViewArtefactLow.Text = "";
        }

        private List<string> Shuffle(List<string> list)
        {
            var result = new List<string>();
            var rg = new Random();
            var cnt = rg.Next(3);

            while (list.Count != 0)
            {
                var i = rg.Next(list.Count);
                result.Add(list[i]);
                list.RemoveAt(i);
            }

            return result;
        }
    }
}