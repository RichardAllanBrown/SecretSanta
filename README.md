SecretSanta
===========

A simple, Secret Santa generator. Uses generics to allow you to easily tailor it's use.

It is based off of a [gist](https://gist.github.com/RichardAllanBrown/7375465) I did in Scala a little while ago. This approach involves taking a list of participants, shuffles them and then zips them to the same list shuffled again. Any invalid pairings (such as getting yourself) or any pairing in the banned list (to prevent, say, spouces from getting one another) will exclude that entire permutation.

By keeping it generic, it is up to the user to implement it how they want to, it could be integrated with an eMail to automatically send out who is the secret santa to who while maintaining secrecy. There is a simple file based console app included to show how it works / is to be used.
