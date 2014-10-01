
[Back to repository]

Coding conventions
------------------------------------------------------------------------------------------------
- Definitions
```sh
  Exposure struct ThisIsAStruct { }

  Exposure ThisIsAClass {
  }
  Exposure ThisIsASubClass: ThisIsAClass {
  }
  
  Exposure type ThisIsAMethod(type arg) {
  }
  Exposure type ThisCouldBeAMethodWithASingleStatement(type arg) { }
  
  Exposure const type THIS_IS_A_CONST_VAR
  private type thisIsAPrivateVar
  Exposure type ThisIsANonPrivateVar
  
  Exposure type ThisIsAProperty {
    get { return var; }
    set { var = value; }
  }
```
- Statements
```sh
  if (booleanExpr) DoStuff();
  if (booleanExpr)
    DoStuff();
  if (booleanExpr) {
    DoStuff1();
    DoStuff2();
  }
  if (booleanExpr) DoStuff();
  else if (booleanExpr) DoStuff();
  else DoStuff();

  if (booleanExpr)
    DoStuff();
  else if (booleanExpr)
    DoStuff();
  else
    DoStuff();

  if (booleanExpr) {
    DoStuff1();
    DoStuff2();
  }
  else if (booleanExpr){
    DoStuff1();
    DoStuff2();
  }
  else DoStuff();
  
  while (booleanExpr) DoStuff();
  while (booleanExpr)
    DoStuff();
  while (booleanExpr) {
    DoStuff1();
    DoStuff2();
  }
  
  for (...) DoStuff();
  for (...)
    DoStuff();
  for (...) {
    DoStuff1();
    DoStuff2();
  }

  try {
  	DoStuff();
  } catch(Expection e) {
  	DoExceptionStuff();
  } finally {
  	DoFinallyStuff();
  }

  Switch(var) {
  	case val1:
  		DoStuff1();
  		break;
	case val2:
  		DoStuff2();
  		break;
  	default://Always use the default keyword. Even if it is not supposed to enter there, just log an error message in there
  		DoStuffDefault();
  		break;
  }

  //Use this
  variable = (bool) ? var1 : var2;
  //or
  variable = (bool) ? var1
  					: var2;
  //instead of this
  if (bool) variable = var1;
  else variable = var2;
```
- Script hierarchy
```sh
external using statements

namespace Some.Namespace.Path {
    internal using statements

    public class Classname: PossibleSuperClass {
  	public consts
  	protected consts
  	private consts

  	public vars
  	protected vars
  	private vars

  	constructors

  	public properties
  	protected properties
  	private properties

  	Other methods in proper #region #endregion based on relevance

  	Unity methods if inherits Monobehavior (private if not overridable)

  	Generally try to avoid the above and find more generic ways of coding but if very needed
  	child classes
    child structs
    child enums
    }
}

```
- Other
  - Use #region ... #endregion only to group multiple definitions
  - Every script has to be included into a namespace
  - to be continued

[Back to repository]

[Back to repository]: https://github.com/PokemonNxtDevStudio/PokemonNXTPhoton