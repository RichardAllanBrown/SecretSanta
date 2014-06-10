SecretSanta
===========

A simple, Secret Santa generator. Uses generics to allow you to easily tailor it's use.

It is based off of a [gist](https://gist.github.com/RichardAllanBrown/7375465) I did in Scala a little while ago. This approach involves taking a list of participants, shuffles them and then zips them to the same list shuffled again. Any invalid pairings (such as getting yourself) or any pairing in the banned list (to prevent, say, spouces from getting one another) will exclude that entire permutation.

By using generics, it is up to the user to implement it how they want to.  You may choose to have a class describing each participants eMail address and then EMail who they are the Secret Santa for. Alternativly, you could use twitter handles and direct messaging to tell who is who, there are many possibilities.

Performance
-----------

The algorithm is nothing special. Since one of the aims was to allow limiting of certain gifter / giftee pairings, it can run in the terrifyingly awful time of O(n!) in the worst case scenario (no set of pairings is valid). However, it's practically much faster.
