# Change Log

## [2.3.1]

**Fixed:**

- Lang.Extensions: OrDie() funcion now works with different types
- IO.PathUtils: SafeCombine is replaced by GetSafePath to return full path


## [2.3.0]

**Added:**

- Lang.Extensions: OrDie() funcion to quickly check for null parameters
- Drawing.ImageUtils: GetImage() loads image from disk and supports color management
- IO.FileUtils: Adds sanitization for file names
- IO.PathUtils: Adds a funcion to avoid usage of existing filenames
- Collections.LinqUtils: Adds TakeLast(n) items from a collection

**Moved from Preview:**

- IO: TempFile, a disposable component to  write/delete temporary file in a small time lapse
- IO.PathUtils: Clone() for duplicating direcory trees
- Drawing.Extensions: StringExtensions to convert a string into a Bitmap


## [2.2.0] (2018-07-30)

**Added:**

- IO.DirectoryUtils: Utility and extension methods for directories
- Lang.StringUtils: Extract - Method to find and get a subtring between text
- Collections.CollectionsUtils: ToChunk - Method to split an array into smaller parts

##  [2.1.0] (2017-11-06)
**Fixed:**

- Collections.CollectionExtensions: Bug when shifting value types

**Added:**

- Drawing.ImageUtils: Load image from bytes
- Drawing.ImageUtils: Extension to convert image to base64
- Collections.EnumerableUtils: IEnumerable IsNullOrEmpty extension
- Collections.CollectionUtils: ICollections AddRange, Sort extension

##  [2.0.3] (2017-08-08)

**Added:**

- Security.Hashing: new Static methods for quick hash
- Security.Hashing: SHA512 algorythm implementation
- Lang.DateTimes: Some datetime constraints

**Released:**

- Runtime.Serialization: Binary serializer

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