# Grappachu.Core 

The purpose of this project is to have my personal collection of utilities and interfaces grouped in a multi-framework package free from dependencies. 

## Getting Started
You can simply download the package and explore the classes.
I'll try to keep Namespaces used very close to Microsoft choiches so it shoul be esier to fine what you're looking for.

Anyway here's a small list of namespaces with some features in the library:

* **Collections** 
  * IEnumerable extension methods
* **Drawing** 
  * ImageExtensions: A set of extensions for resize and scaling Bitmaps
  * TableLayoutEngine: Algorythm to compute column weights of DataTable
* **Globalizations**
  * FileSizeFormatter: Formatter for printing file sizes in a friendly way
* **Lang**
  * Various extensions for common types
* **Media**
  * Various interfaces for media components
* **Messaging**
  * Basic interfaces for working on a message queue
* **Runtime**
  * Compilers: Providers for compiling and running c# code at runtime
* **Security**

----------------------------

**Be aware from ```Grappachu.Core.Preview``` namespace! This is only for unreleased classes and averything inside of it can change in signatures (and of course namespace) without advice.**

----------------------------

# Change Log

## [2.0.2] (2017-06-12)

**Added:**
- Messaging namespace with basic interfaces

**Released:**
- C# Compiler

## [2.0.1] (2017-06-05)

**Preview**
- C# Compiler (Runtime.Compilers.CsCompiler)

**Added:**
- Changelog and documentation

**Released:**
- String extension to Wrap text to multiple lines


## [2.0.0](https://github.com/grappachu/core/commit/3e12b2f84cbb714d6eff93be4684b2fe93929d8a) (2017-06-05)

**Changed:**
- Moved most of files to match a best namespace
- Some classes moved to Preview namespace

## [1.0.0](https://github.com/grappachu/core/commit/cec2b0d5dbbd5e8703487045f751f28294b4dbf3) (2017-04-23)

**Added:**
- First Version of package with various utilties