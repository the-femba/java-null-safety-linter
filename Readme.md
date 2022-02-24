# Null Safety Linter

Null safety static analizer (linter) for java language.

# How Use

Открой и пой.

# Language Support

## Conditions

## Functions

Functions outside of class.

```java
void myFunc1() { }
```

```java
void myFunc2(string text) { }
```

```java
void myFunc2(string text);
```

Doesn't supports neasted functions beacouse java isnt support it.

## Variables

Create of variablee and equals ti variable;

```java
string text = "1";
text = "2";
```

## Annotations

Support only annotations for variables and arguments and without constructor.

```java
void myFunc1() { 
  @NotNull string text = "";
}
```

```java
void myFunc2(@NotNull string text) { }
```

## Cascade Invoke

```java
myFunc2("text").toSomething().value;
```

## If

```java
if (value != null) {

}
else if (value > 2) {

}
else {

}
```

Dosen't support simple if statement like ```if (value > 2) myFun1();```.

## Switch

```java
switch (value) {
  case 1: myFun1(); break;
  default: myFun2(); break;
}
```

Work with break only.

Dousent suppport double statements like

```java
switch (value) {
  case 0:
  case 1: myFun1(); break;ё
}
```

# Support

- [x] Formatter
  - [x] Remove comments
- [x] Lexer
  - [x] Lex single file
  - [x] Lex single simple parts of code
- [ ] Patterns (lexer)
  - [x] Symbols patter
    - [x] \>
    - [x] \<
    - [x] \=
    - [x] \>=
    - [x] \<=
    - [x] \==
    - [x] \!=
    - [x] \:
    - [x] \.
    - [x] \;
    - [x] \+
    - [x] \*
    - [x] \/
    - [x] \{
    - [x] \}
    - [x] \)
    - [x] \(
    - [x] \@
  - [ ] Keywords patter
    - [x] if
    - [x] else
    - [x] new
    - [x] return
    - [x] switch
    - [x] case
    - [x] default
    - [x] break
    - [ ] class
    - [ ] this
    - [ ] base types, like int, string
  - [x] Literals Pattern
    - [x] int (1, 323)
    - [x] double (1.323, 323.234)
    - [x] char (' ', 't')
    - [x] string ("test")
  - [x] Types Pattern
  - [x] Names Pattern
- [ ] Rules (lexer)
  - [ ] If rule
- [ ] Parser
- [ ] Static analyzer
- [ ] Features (analyzer)
  - [ ] NullSafe features

# Architecture

This is planed architecture.

![Bebra](Assets/plan-structure.png)

#

All rights reserved.

Developed by [@femboy-dev](https://github.com/femboy-dev).
