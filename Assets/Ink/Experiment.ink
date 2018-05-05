-> header

// This is a comment. It won't show up
// You can define global variables wherever. The two lines below *should* run on compile, even though the same will never reach them.
VAR global_variable = true
VAR global_int = 0
VAR action = -1
// Global variables can be read from Unity using the Story.variablesState object.

=== header
You hear someone walk up behind you
"Hello!"
*	"Do I know you?"[] I ask
	(After choosing that option, we continue here...)
*	Here's another option.
*	This appears in the option text and the resulting text [this is only in the option text] and this is only in the resulting text
	Dialogue contunies...
	Continues some more...
	** 	Two asterisks. We have nested choices! -> other_section
	** 	"Another option[."]," I said. This option will set some variables
		~ global_variable = false // Note the ~
		~ global_int = 1 + (global_int * 2)
	**	["How many options are there!?"] I couldn't believe how many options there were... And this one didn't even change any variables.
	--	This is a gather (minus sign(s)) - all options at this level will go here if they have nowhere to go.
	{ global_int > 0: 
		We DID change a variable!
	-else:
		The variables remain stubbornly unchanged...
	}
- -> END
	
	
	
=== other_section
And now we've ended up in another section
-> DONE