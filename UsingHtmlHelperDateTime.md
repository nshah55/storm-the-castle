Quick introduction to `Html.DateTime()`

# Introduction #

I want to talk about Date only but somehow .NET doesn't have Date data type so here we discuss about DateTime but focus on Date part only.

# Details #

Depend on where you living the date written 12/2/2009 could mean 2/12/2009 there we got problem:
  * How can you tell the different 12 Feb 2009 or 2 Dec 2009?
  * What is the best UI experience for user to input Date value?

The quick & dirty solution is to hard code day, month, year there we save some trouble time figure out which part are day, month.

**note** _if you properly configure your server & client setting a single text box for date entry should be sufficient._

# `Html.DateTime()` #

Now by using `Html.DateTime()` helper method at run time it will generate 3 select boxes which allow you to input date value from pre-design list.