**helper-net** [![Build status](https://img.shields.io/appveyor/ci/bitbeans/helper-net.svg?style=flat-square)](https://ci.appveyor.com/project/bitbeans/helper-net) [![Build Status](https://img.shields.io/travis/bitbeans/helper-net.svg?style=flat-square)](https://travis-ci.org/bitbeans/helper-net) [![NuGet Version](https://img.shields.io/nuget/v/helper-net.svg?style=flat-square)](https://www.nuget.org/packages/helper-net/) [![License](http://img.shields.io/badge/license-MIT-green.svg?style=flat-square)](https://github.com/bitbeans/helper-net/blob/master/LICENSE.md)

A small collection of useful helper methods that I use in several projects.


# Status #

**Pull requests and/or optimization proposals are always welcome!**

# Scope #

## ArrayHelper ##

```csharp 
static T[] ConcatArrays<T>(params T[][] arrays)
```

```csharp 
static T[] ConcatArrays<T>(T[] arr1, T[] arr2)
```

```csharp 
static T[] SubArray<T>(T[] arr, int start, int length)
```

```csharp 
static T[] SubArray<T>(T[] arr, int start)
```

```csharp 
static bool ConstantTimeEquals(byte[] a, byte[] b)
```

## ConvensionHelper ##

```csharp 
static byte[] IntegerToLittleEndian(int data)
```

## CryptoHelper ##

```csharp 
static byte[] Xor(byte[] data, IReadOnlyList<byte> keys)
```

## StreamHelper ##

```csharp 
static byte[] ReadFully(Stream input)
```

## ShellHelper ##

```csharp 
static string Escape(string argument, bool quote = false)
```

```csharp 
static string ExecuteShellCommand(string filename, string arguments, int timeout = 9000)
```

## PaddingHelper ##

```csharp 
static byte[] AddPkcs7(byte[] data, int paddingLength)
```

```csharp 
static byte[] RemovePkcs7(byte[] paddedByteArray)
```

```csharp 
static byte[] AddZero(byte[] data, int paddingLength)
```

```csharp 
static byte[] RemoveZero(byte[] paddedByteArray)
```

## RandomHelper ##

### SecureRandomProvider ###

```csharp 
static int Next()
```

```csharp 
static int Next(int maxValue)
```

```csharp 
static int Next(int minValue, int maxValue)
```

```csharp 
static void GetBytes(byte[] data)
```

```csharp 
static void GetNonZeroBytes(byte[] data)
```

### RandomProvider ###

```csharp 
static int Next()
```

```csharp 
static int Next(int maxValue)
```

```csharp 
static int Next(int minValue, int maxValue)
```

```csharp 
static void GetBytes(byte[] data)
```

### Well512RandomProvider ###

```csharp 
static int Next()
```

```csharp 
static int Next(int maxValue)
```

```csharp 
static int Next(int minValue, int maxValue)
```

## License
[MIT](https://en.wikipedia.org/wiki/MIT_License)