﻿using System;
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
    public class GameStateChangedEventArgs
    {
     
        public GameState state { get; }

        public GameStateChangedEventArgs( GameState st)
        {

            state = st;
        }
    }
}