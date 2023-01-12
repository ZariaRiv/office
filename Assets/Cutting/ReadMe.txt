[19/12/2022] https://github.com/BLINDED-AM-ME/UnityAssets
open-sourced
-> All in objects is a |noun|
-> All in extension is |noun| with extension

Goal: Make the minigame work
Approach: First describe minigame. What is winning condition and what is lose and what is intermediate.
-> Really informal. Time constrained

Description: Cut the wood until all desired pieces have been cut.
Win: When all pieces are correctly cut out.
Lose: When one of the pieces has been cut.

Question: How to detect whether pieces have been cut out correctly?
-> Get all hittables and look whether their any which do have the same gameobject being attached to
    -> But first get all objects which attached to

Question: How to detect whether pieces have been cut?
-> Maybe make Observer Design Pattern which gives through which objects are now being cut

Remark:
* More questions or |Goal-Approach| needed