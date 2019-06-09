# XRE_GroceryStore
XRE Coding Challenge, Online Grocery Store

I made a couple of assumptions for this project, as the specifics details were not outlined.

First, since the brief explicitly said: "Each order should contain the minimal number of packs", as such my solution prioritizes delivering the smallest total number of packs over achieveing an exact match, this can be seen clearly in the 6th test example.
It does however attempt to minimize excess product, provided that it will not increase total packs sent, this can be seen in test example 7.

The second assumption I made is that the format for input should be a uniform 'PRODUCT' then 'NUMBER' in full uppercase. I set it this way on the premise that the actual input would be sent by another computer, not hand-written by a user.
However, the program is set up to catch various forms of invalid input to avoid simple input mistakes.
