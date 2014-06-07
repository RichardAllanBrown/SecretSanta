SecretSanta
===========

A simple, Secret Santa generator. Uses generics to allow you to easily tailor it's use.

Initially based off of a (gist)[] I did in Scala a little while ago, this approach involves taking a list of participants, shuffles them and then zips them to the same list shuffled again. Any invalid pairings (such as getting yourself) or any pairing in the banned list (to prevent, say, spouces from getting one another) will exclude that entire permutation.
