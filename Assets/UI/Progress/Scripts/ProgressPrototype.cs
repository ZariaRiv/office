/**
 * Name: Laurence van Leuken
 * Date: 10/12/2022
 * Code: Progress Prototype
 */

/// Prototype of progress using Template Pattern
/// Source: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed
/// Source: https://www.softwaretestinghelp.com/csharp-using-statement-virtual-method/#:~:text=The%20virtual%20keyword%20in%20C%23,is%20preceded%20by%20override%20keyword.
/// -> A virtual method must be overriden and the normal method not (in C#)
/// -> Therefor, a normal method implementation can change depending on the type and if no overriding is used.
/// -> Never had difficulty overriding existing methods, but maybe that was all accidental?
/// Source: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGP {

    public abstract class ProgressPrototype {

        [SerializeField]
        [Range(0f, 1f)]
        private float progress;

        // Template method
        public void setProgress(in float currentProgress) {
            progress = currentProgress;
            progressChanged();
        }

        // Primitive method => Often gets changed. So abstract it is (otherwise use virtual)
        protected abstract void progressChanged();

        public float getProgress() {
            return progress;
        }

        public static implicit operator float (in ProgressPrototype value) => value.getProgress();
    }
}
