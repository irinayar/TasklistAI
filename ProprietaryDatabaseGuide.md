# Proprietary Database Guide — mDataProprietary.vb

This guide explains how to enable Oracle and InterSystems (IRIS / Caché) database support in the `mDataProprietary.vb` file located in the `App_Code` folder.

By default, all proprietary database code is commented out. If you hold a valid license for Oracle or InterSystems, follow the steps below to activate the provider you need.

---

## License Requirements

**Oracle Database** requires a valid commercial license from Oracle Corporation. You also need to install the Oracle Managed Data Access NuGet package (`Oracle.ManagedDataAccess`). Visit https://www.oracle.com for licensing.

**InterSystems IRIS** requires a valid commercial license from InterSystems Corporation. You also need the InterSystems IRIS .NET driver (`InterSystems.Data.IRISClient`). Visit https://www.intersystems.com for licensing.

**InterSystems Caché** requires a valid commercial license from InterSystems Corporation. You also need the InterSystems Caché .NET driver (`InterSystems.Data.CacheClient`).

---

## File Overview

`mDataProprietary.vb` is a VB.NET module that contains database access functions for Oracle, InterSystems IRIS, and InterSystems Caché. It is organized into five regions, each containing three functions (one per database engine):

| Region | Functions | Purpose |
|--------|-----------|---------|
| HasRecords | `HasRecords_IRIS`, `HasRecords_Cache`, `HasRecords_Oracle` | Execute a query and return results in a DataTable |
| CountOfRecords | `CountOfRecords_IRIS`, `CountOfRecords_Cache`, `CountOfRecords_Oracle` | Execute a count query and return results |
| mRecords | `mRecords_IRIS`, `mRecords_Cache`, `mRecords_Oracle` | Execute a query with DISTINCT handling and return results |
| RunSP | `RunSP_IRIS`, `RunSP_Cache`, `RunSP_Oracle` | Execute a stored procedure with parameters |
| DatabaseConnected | `DatabaseConnected_IRIS`, `DatabaseConnected_Cache`, `DatabaseConnected_Oracle` | Test if a database connection can be opened |

Every function body is commented out with single-quote (`'`) line comments. You only need to uncomment the functions for the database engine you are using.

---

## Enabling Oracle

### Step 1: Uncomment the Imports statement

At the top of the file (line 2), change:

```vb
'Imports Oracle.ManagedDataAccess.Client
```

to:

```vb
Imports Oracle.ManagedDataAccess.Client
```

### Step 2: Uncomment all Oracle function bodies

There are five functions to uncomment. In each one, remove the `'` comment character from every line between `Try` and `End Try`.

**HasRecords_Oracle** (lines 81–99) — remove `'` from lines 81–98:

```vb
' BEFORE (commented):
    'Try
    '    Dim myConnection As Oracle.ManagedDataAccess.Client.OracleConnection
    '    ...
    'End Try

' AFTER (uncommented):
    Try
        Dim myConnection As Oracle.ManagedDataAccess.Client.OracleConnection
        ...
    End Try
```

**CountOfRecords_Oracle** (lines 169–190) — remove `'` from lines 171–188.

**mRecords_Oracle** (lines 280–305) — remove `'` from lines 282–303.

**RunSP_Oracle** (lines 370–401) — remove `'` from lines 372–399.

**DatabaseConnected_Oracle** (lines 456–476) — remove `'` from lines 458–474.

### Step 3: Install the Oracle driver

Place the `Oracle.ManagedDataAccess.dll` in your application's `Bin` folder, or install it via NuGet:

```
Install-Package Oracle.ManagedDataAccess
```

### Step 4: Configure web.config

Make sure your `web.config` has the Oracle connection strings uncommented and the Oracle provider flag set:

```xml
<add key="Oracle" value="OK" />
```

---

## Enabling InterSystems IRIS

### Step 1: Uncomment all IRIS function bodies

There are five functions to uncomment. In each one, remove the `'` comment character from every line between `Try` and `End Try`.

**HasRecords_IRIS** (lines 7–41) — remove `'` from lines 9–39.

**CountOfRecords_IRIS** (lines 104–134) — remove `'` from lines 106–132.

**mRecords_IRIS** (lines 194–233) — remove `'` from lines 196–231.

**RunSP_IRIS** (lines 309–336) — remove `'` from lines 311–334.

**DatabaseConnected_IRIS** (lines 405–427) — remove `'` from lines 407–425.

### Step 2: Install the IRIS driver

Place `InterSystems.Data.IRISClient.dll` in your application's `Bin` folder. This DLL is included with your InterSystems IRIS installation, typically found at:

```
C:\InterSystems\IRIS\dev\dotnet\bin\v4.5\InterSystems.Data.IRISClient.dll
```

### Step 3: Configure web.config

Make sure your `web.config` has the IRIS connection strings uncommented and the IRIS provider flag set:

```xml
<add key="IRISProv" value="OK" />
```

---

## Enabling InterSystems Caché

### Step 1: Uncomment all Caché function bodies

There are five functions to uncomment. In each one, remove the `'` comment character from every line between `Try` and `End Try`.

**HasRecords_Cache** (lines 43–77) — remove `'` from lines 45–75.

**CountOfRecords_Cache** (lines 136–167) — remove `'` from lines 138–165.

**mRecords_Cache** (lines 235–278) — remove `'` from lines 237–276.

**RunSP_Cache** (lines 338–368) — remove `'` from lines 340–366.

**DatabaseConnected_Cache** (lines 429–454) — remove `'` from lines 431–452.

### Step 2: Install the Caché driver

Place `InterSystems.Data.CacheClient.dll` in your application's `Bin` folder. This DLL is included with your InterSystems Caché installation, typically found at:

```
C:\InterSystems\Cache\dev\dotnet\bin\v4.5\InterSystems.Data.CacheClient.dll
```

### Step 3: Configure web.config

Make sure your `web.config` has the Caché connection strings uncommented and the Caché provider flag set:

```xml
<add key="CacheProv" value="OK" />
```

---

## How to Uncomment Code in Visual Studio

1. Open `mDataProprietary.vb` in Visual Studio.
2. Select all the commented lines within a function body (from `'Try` to `'End Try`).
3. Press **Ctrl+K, Ctrl+U** to uncomment the selected lines.
4. Repeat for each function you need to enable.

Alternatively, use Find and Replace (**Ctrl+H**) within the selected block:
- Find: `    '    ` (four spaces, apostrophe, four spaces)
- Replace with: `        ` (eight spaces)

---

## Quick Reference: What to Uncomment

| If you have a license for | Uncomment the Imports | Uncomment these functions |
|---------------------------|----------------------|---------------------------|
| Oracle | Line 2: `Imports Oracle.ManagedDataAccess.Client` | `HasRecords_Oracle`, `CountOfRecords_Oracle`, `mRecords_Oracle`, `RunSP_Oracle`, `DatabaseConnected_Oracle` |
| InterSystems IRIS | (none needed) | `HasRecords_IRIS`, `CountOfRecords_IRIS`, `mRecords_IRIS`, `RunSP_IRIS`, `DatabaseConnected_IRIS` |
| InterSystems Caché | (none needed) | `HasRecords_Cache`, `CountOfRecords_Cache`, `mRecords_Cache`, `RunSP_Cache`, `DatabaseConnected_Cache` |

You can enable multiple databases at the same time if you hold licenses for more than one.

---

## Troubleshooting

**"Type 'OracleConnection' is not defined"** — The `Imports Oracle.ManagedDataAccess.Client` line at the top of the file is still commented out, or `Oracle.ManagedDataAccess.dll` is missing from the `Bin` folder.

**"Type 'IRISConnection' is not defined"** — `InterSystems.Data.IRISClient.dll` is missing from the `Bin` folder.

**"Type 'CacheConnection' is not defined"** — `InterSystems.Data.CacheClient.dll` is missing from the `Bin` folder.

**Functions return empty string but don't execute queries** — The function body is still commented out. The default (commented) behavior returns an empty string without doing any database work.

**"INCORRECT LIST FORMAT" errors with InterSystems** — This is a known internal InterSystems error that does not affect data retrieval. The IRIS and Caché functions already handle this by catching and ignoring this specific exception.

**"USE_DISTINCT" returned from mRecords_IRIS or mRecords_Cache** — InterSystems handles DISTINCT queries differently. The application will automatically fall back to an alternative query method when this is returned.
