# Deltics.PeImageInfo

This project was inspired by a problem with the provided `FileVersionInfo` api in .net core.

Despite being available on all .net supported platforms, the underlying implementation of `FileVersionInfo` relies (or relied) on Windows native apis to extract version information from [**Windows Portable Executable (PE)**](https://docs.microsoft.com/en-us/windows/win32/debug/pe-format) images.  

Having a scenario where I needed to extract such information whilst running on *nix platforms (the information still being contained in Windows PE files), I created this library for navigating the data structures in such files (Windows EXEs and DLLs).

In addition, the `FileVersionInfo` api required a file on disk; in my scenario I had a file image received in a byte array or stream and so I desired a solution that would enable me to work directly with that byte array or stream without having to first persist to a file system.

This library is (part of) the result.

It provides only the basic information from a PE file image.  That is, the **DOS Header**, **COFF Header** (including the so-called **Optional Header**) and the **Section Table**.


## Usage

Once you have added the package as a dependency, simply new-up a `PeImage` object, passing the constructor a  stream containing the PE image of interest:

```
   var pe = new PeImage(new FileStream(..));
```


### Building On A PeImage

Further libraries build on this basic package, providing facilities for working with specific content in a PE file image, such as the resource directory, describing any resources (icons, strings, version information etc).  These in turn are built on by yet more libraries, providing further facilities for working with specific resource types such as the [VERSIONINFO](https://docs.microsoft.com/en-us/windows/win32/menurc/versioninfo-resource) resource that started the whole project!

To make implementation of such additional libraries a little easier, the `PeImage` object maintains the internal reader that was used to initially load the image info from the stream.  This avoids the need to create further readers when navigating the data structures described in the various PE headers.

```
    var pe = new PeImage(..);
    var reader = pe.Reader;
```

This internal reader is exposed via a public property called `Reader`; this object is based on the `BinaryReader` class.

**NOTE:** _If the stream passed to the constructor of `PeImage` is not a `MemoryStream`, the stream is first copied to a new `MemoryStream` and it is this in-memory stream that the reader will then work with_.


### Reading Strings in a PE Image

Two methods are provided on `PeReader`, in addition to those inherited from `BinaryReader`:

* ReadPadding()
* ReadStringZ()

#### ReadPadding()

`ReadPadding()` reads bytes from the stream until the stream position is on a 32-bit boundary.  This is used to ensure that the stream read position is set correctly when reading data structures that contain values aligned on such boundaries.

Note that if the stream position is already on a 32-bit boundary when this method is called then the position is not advanced any further.


#### ReadStringZ()

`ReadStringZ` has two overloads, one of which accepts a length parameter and the other which takes no parameters.

`ReadStringZ()` will read a null-terminated string from the underlying stream at the current stream position.  Characters will be read from the stream until a null-terminator is encountered.

```
   var s = reader.ReadStringZ();    // 's' will hold the null-terminated string at the current position
                                    //  in the stream, excluding null terminator
```

`ReadStringZ(len)` will read the specified number of Utf16 codes from the stream at the current stream position **and** will also read what is expected to be a null terminator (two-bytes) following those Utf16 codes.  If the two bytes following the specified number of codes are not null, then an **InvalidOperationException** is thrown.

```
   var s = reader.ReadStringZ(10);  // 's' will hold the null-terminated string of length == 10 at the
                                    //  current position in the stream (or exception thrown)
```


## A Word On Tests

The tests for this library rely on known PE images that are built and embedded into the test project as artefacts.

These artefacts are compiled by Delphi for both x86 and x64 images for both application (EXE) and DLL images.