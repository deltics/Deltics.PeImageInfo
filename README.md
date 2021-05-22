# Deltics.PeImageInfo

This project was inspired by a problem with the provided `FileVersionInfo` api in .net core.

Despite being available on all .net supported platforms, the underlying implementation of `FileVersionInfo`
relies (or relied) on Windows native apis to extract version information from [**Windows PE** (portable executable)]
(https://docs.microsoft.com/en-us/windows/win32/debug/pe-format) images.

Having a scenario where I needed to extract such information whilst running on *nix platforms (the information
still being contained in Windows PE files), I created this library for navigating the data structures in such files
(Windows EXEs and DLLs).

In addition, the `FileVersionInfo` api required a file on disk; in my scenario I had a file image received in a
byte array or stream and so I desired a solution that would enable me to work directly with that byte array or
stream without having to first persist to a file system.

This library is (part of) the result.

It provides only the basic information from a Portable Executable file image.  That is, the DOS Header, COFF Header
(including the so-called Optional Header) and the section table.


## Usage

Once you have added the package as a dependency, simply new-up a PeImage object, passing to the constructor a 
stream containing the PE image of interest:

```
   var pe = new PeImage(new FileStream(..));
```


### Building On A PeImage

Further libraries then provide further facilities for working with specific content in a PE file image, such as
any resources (icons, strings, version information etc) and then specific resource types such as the [VERSIONINFO]
(https://docs.microsoft.com/en-us/windows/win32/menurc/versioninfo-resource) resource that started the whole project!

To make implementation of such additional libraries a little easier, the `PeImage` object maintains the internal reader
that was used to initially load the image info from the stream.  This avoids the need to create further readers when
navigating the data structures described in the various PE headers.

```
    var pe = new PeImage(..);
    var reader = pe.Reader;
```

This internal reader is exposed via a public property called `Reader`; this object is based on the `BinaryReader` class.

NOTE: _If the stream passed to the constructor of `PeImage` is not a `MemoryStream`, the stream is first copied
to a new `MemoryStream` and it is this in-memory stream that the reader will then work with_.


### Reading Strings in a PE Image

In addition to the usual methods provided by `BinaryReader` from which `PeReader` extends, a `ReadStringZ` method is
provided.  This has two overloads, one of which accepts a length parameter and the other which takes no parameters.

The parameter-less overload will read a null-terminated string from the underlying stream at the current stream
position.  Characters will be read from the stream until a null-terminator is encountered.

```
   var s = reader.ReadStringZ();    // 's' will hold the null-terminated string at the current position
                                    //  in the stream, excluding null terminator
```

The overload that takes a length parameter will read the specified number of characters from the stream at the
current stream position **and** will also read what is expected to be a null terminator (two-bytes).  If the
two bytes following the string of the specified length are not null, then an **InvalidOperationException** is thrown.

```
   var s = reader.ReadStringZ(10);  // 's' will hold the null-terminated string of length == 10 at the
                                    //  current position in the stream (or exception thrown)
```


## A Word On Tests

The tests for this library rely on known PE images that are built and embedded into the test project as artefacts.

These artefacts are compiled by Delphi for both x86 and x64 images for both application (EXE) and DLL images.