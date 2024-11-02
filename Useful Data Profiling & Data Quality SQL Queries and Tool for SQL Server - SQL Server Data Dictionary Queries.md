This article collects useful **data profiling** SQL queries for **SQL Server** that allow you to discover data and test its quality.

Those queries can be used by anyone that has access to the database and has basic knowledge of SQL and are an easy way to start data discovery and quality management. When you see first results and see the need to scale your efforts then I present you also the tool that will make the process more convenient: [Dataedo](https://dataedo.com/product/data-profiling).

## What is Data Profiling

Data Profiling is the process analyzing datasets and creating useful summaries that help in discovery and understanding of its structure, characteristics, meaning and quality.

### Data profiling ≠ data analysis

Data profiling is not data analysis. Purpose of **data analysis** is to get information, knowledge, or insights about processes represented by the data (such as sales, customers, products, etc.). The purpose of **data profiling** is to get information and knowledge about the data itself.

### Why you should care?

Data profiling creates information that help you with the following:

1.  **Discover data** - data profiling helps you discover data you have.
2.  **Understand data** - seeing value distribution, characteristics or typical values of particular column can lead you to discovering its purpose.
3.  **Trust data** - knowing minimum, maximum, typical values or number of null values in a column will give you more confidence in the data (or help identify issues).
4.  **Avoid errors** - when we analyze data we make a number of assumptions, such as that _invoice.amount_ is non-null positive number, _document.status_ has only 'A' and 'D' values, and so on. If those assumptions are incorrect analysis or report results will be incorrect. Data profiling helps us avoid such assumptions.
5.  **Improve data quality** - it's hard to manage something that you cannot measure. Data profiling allows you to identify and measure issues with data.
6.  **Document the data** - having an x-ray of columns in a table helps you understand and document the table.

### Use cases of data profiling

1.  **Data Quality/Data Governance** - Data profiling is plays crucial role in data quality - this is how you asses the quality of the data.
2.  **Data Warehousing** - according to Kimball, father of data warehousing, data profiling should be performed throughout the data warehouse project. It is crucial for finding and understanding data in source systems, and assessing quality and cleansing the data.
3.  **BI & Analytics** - any data analysis requires good understanding of data being analyzed. Even great analysts with extensive experience with your databases make wrong assumptions about the data. Data profiling helps everyone better find and understand the dataset.
4.  **Data or Application Migration** - Migration of data to a new database, application or cloud requires good understanding of the data in source database. Analysts and developers need to map source and destination databases, and design transformation and cleansing logic. And that requires understanding the structure, types, values and quality of data. Data profiling should be one of the first steps in the process, followed by data documentation.
5.  **System or Application Integration** - Similarly to data migration, integration of systems requires good understanding of data structures, fields, datatypes, allowed values of systems being integrated.
6.  **Data Protection** - data profiling helps you identify sensitive data in your databases.

## Data profiling queries for SQL Server

Below you'll find copy&paste&replace queries that you can use on your database to profile data in columns and understand data and test its quality.

**How to use queries?**

Each query is run against specific table or column. To use it on your database replace _'mytable'_ and _'mycolumn'_ with names of particular table and/or column.

### Count rows in a table

Query below returns number of rows in a table:

```
SELECT COUNT(*) AS row_count
FROM mytable
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/22e702c0890e63bc308f6497df678b5b.png#center "Image title")

### Min/max/avg and more for numbers & dates

This query analyzes data in specific (numeric or date) column and finds:

1.  Minimum value,
2.  Maximum value,
3.  Average value,
4.  Standard deviation - how far to the average are values in the column,
5.  Variance.

For many fields (i.e. age, order\_date, amount, etc.) you know what the expected range of values is. This query will help you identify outliers and incorrect values.

```
SELECT MIN(mycolumn) AS value_min, 
       MAX(mycolumn) AS value_max, 
       AVG(mycolumn) AS value_avg, 
       STDEV(mycolumn) AS value_stddev, 
       VAR(mycolumn) AS value_var
FROM mytable
WHERE mycolumn IS NOT NULL
```

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/data-profiling-data-quality/87cd4c0863d53693a734ede3f928befd.png#center "Image title")

Column profiling (min, max, avg) in [Dataedo](https://dataedo.com/product/data-profiling):

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/data-profiling-data-quality/f49c0b1b6703e0110f83787e3cdceef9.png#center "Image title")

[Try Dataedo Data Profiling for free](https://dataedo.com/free-trial)

### Min/max string

This is a version of the query above for strings - returns minimum and maximum strings, i.e. first and last string when sorted alphabetically (considering table/database collation):

```
SELECT MIN(mycolumn) AS value_min_string, 
       MAX(mycolumn) AS value_max_string
FROM mytable
WHERE mycolumn IS NOT NULL
AND LEN(mycolumn) &gt; 0
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/11c62f54e43be10a8776886e3a9a6da1.png#center "Image title")

### Test range

If you know what is the allowed range of values for particular column you can use query below to test how many rows fall within and outside that range. Remember to define min/max values (and change data type if needed) in declaration section:

```
DECLARE 
@min int = 10,
@max int = 200;   

SELECT 
    SUM(CASE WHEN mycolumn &lt; @min THEN 1 ELSE 0 end) AS below_range,
    SUM(CASE WHEN mycolumn BETWEEN @min and @max THEN 1 ELSE 0 end) AS within_range,
    SUM(CASE WHEN mycolumn &gt; @max THEN 1 ELSE 0 end) AS above_range
FROM mytable
```

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/data-profiling-data-quality/5445cb7e282a351f03a8cd8d5b6bac8e.png#center "Image title")

### Min/max/avg string length

This query tests length of **non-empty** strings and returns:

1.  Minimum string lengt (non-empty),
2.  Maximum string length,
3.  Average string length,
4.  Standard deviation of string length,
5.  Variance of string length.

```
SELECT MIN(LEN(mycolumn)) AS string_length_min, 
       MAX(LEN(mycolumn)) AS string_length_max, 
       AVG(LEN(mycolumn)) AS string_length_avg, 
       STDEV(LEN(mycolumn)) AS string_length_stddev, 
       VAR(LEN(mycolumn)) AS string_length_var
FROM mytable
WHERE mycolumn IS NOT NULL
AND LEN(mycolumn) &gt; 0
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/360bb1729519a7a9db7e950cf0f16e28.png#center "Image title")

### String length distribution

Query below lists all the distinct lengths of strings in the column and how many rows have string with that length.

```
SELECT LEN(mycolumn) AS string_length,
       count(*) AS row_count
FROM mytable
GROUP BY LEN(mycolumn)
ORDER BY 1
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/b1fcebf7ac2906b43f7508ba2722d675.png#center "Image title")

### Longest and shortest strings

This query lists top 10 longest strings in the column:

```
SELECT TOP 10 mycolumn AS string, 
        LEN(mycolumn) AS string_length
FROM mytable
GROUP BY mycolumn
ORDER BY 2 DESC
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/34b74e6c2cd57aa7360867f95353c24b.png#center "Image title")

This query lists top 10 shortest strings in the column:

```
SELECT TOP 10 mycolumn AS string, 
        LEN(mycolumn) AS string_length
FROM mytable
GROUP BY mycolumn
ORDER BY 2 ASC
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/6c4bb8a9da378663eb84ed7baed106fd.png#center "Image title")

### Most popular values

This query lists top 10 most popular values in the column with information how many rows have that value:

```
SELECT TOP 10
    mycolumn AS value, 
    COUNT(*) row_count
from mytable
WHERE mycolumn IS NOT null
GROUP BY mycolumn
ORDER BY COUNT(*) desc
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/c3a3111c63c1276371856e1c833d4946.png#center "Image title")

Column profiling (top values) in [Dataedo](https://dataedo.com/product/data-profiling):

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/data-profiling-data-quality/ae1658cffd81d7099b3955a0b478b109.png#center "Image title")

[Try Dataedo Data Profiling for free](https://dataedo.com/free-trial)

### Number of distinct values

This query counts how many distinct (unique) values are in the column:

```
SELECT COUNT(DISTINCT mycolumn) AS unique_values_count
FROM mytable
WHERE mycolumn IS NOT NULL
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/b38d8c14f2956abb43107da16415ca6d.png#center "Image title")

### All distinct values

This query lists all the distinct values information how many rows have that value:

```
SELECT 
    mycolumn AS value, 
    COUNT(*) row_count
from mytable
WHERE mycolumn IS NOT null
GROUP BY mycolumn
ORDER BY mycolumn
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/fa15e213e077719641f396093bd8dcc9.png#center "Image title")

### 10 random rows

This query returns 10 random rows.

**Important: \*\*You need to identify id or unique column in the table and replace \*\*id** with its name.

```
SELECT * 
FROM mytable
WHERE id IN (
    SELECT TOP (10) id
    FROM mytable
    ORDER BY NEWID())
```

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/data-profiling-data-quality/8a147727b089f2cfd0957abad3808cc7.png#center "Image title")

### 10 random column values

This query returns 10 random values in the column:

```
SELECT TOP (10) mycolumn
FROM mytable
GROUP BY mycolumn
ORDER BY NEWID()
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/24dafd173f6f495e510d3f2e5bcff4e3.png#center "Image title")

### Null rows - numbers and dates

This query analyzes how many rows are null for number and date columns:

```
SELECT row_type,
    COUNT(*) row_count
FROM 
    (SELECT 
        CASE WHEN mycolumn IS NULL THEN 'Null'
            ELSE 'Non Empty' 
            END AS row_type
        FROM mytable) rows
GROUP BY row_type
```

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/data-profiling-data-quality/628b394f5fa88c6d8c1263486b94576d.png#center "Image title")

### Null and empty rows - strings

This query analyzes which rows are null or empty for the string columns.

Empty string is the one that is not null, but has 0 length or it only consists of spaces.

```
SELECT row_type,
    COUNT(*) row_count
FROM 
    (SELECT 
        CASE WHEN mycolumn IS NULL THEN 'Null'
            WHEN LEN(mycolumn) = 0 THEN 'Empty'
            ELSE 'Non Empty' 
            END AS row_type
        FROM mytable) rows
GROUP BY row_type
```

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/7efb5d9dd77e24b04e83ec2a652a75c8.png#center "Image title")

Column profiling (null, empty, distinct, non-distinct rows) in [Dataedo](https://dataedo.com/product/data-profiling):

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/data-profiling-data-quality/657f2c17bb78bfc63e904e2718b9c183.png#center "Image title")

[Try Dataedo Data Profiling for free](https://dataedo.com/free-trial)

### Test uniqueness (primary/unique keys)

There are many columns in databases that should be (or we assume them to be) unique. Not always database architect or DBA defined unique constraints for those columns. This query helps test whether column is unique.

```
SELECT row_type, 
    SUM(row_count) AS row_count
FROM
    (SELECT 
        CASE WHEN [value] IS NULL then 'NULL'
            WHEN row_count = 1 then 'Unique'
            ELSE 'Non Unique' 
            END AS row_type,
        row_count
    FROM (
        SELECT mycolumn [value], 
            COUNT(*) row_count
        FROM mytable
        GROUP BY mycolumn) X) Y
GROUP BY row_type
```

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/data-profiling-data-quality/9d4684019251dc30d904e6419e08029a.png#center "Image title")

### Test foreign keys (relationships)

Every database has many foreign keys - values in column in one table (foreign table) referring to rows in other tables (primary tables). Think order.product\_id -> products.id. Most database engines support enforcing FK consistency on the database level with **foreign key constraints** but in [many cases they are simply not used](https://dataedo.com/blog/dont-most-databases-have-foreign-keys-constraints) ([there are many reasons for that](https://dataedo.com/blog/why-there-are-no-foreign-keys-in-your-database-referential-integrity-checks)).

This query will allow you to test referential integrity of your database. It will check if there are any orphaned rows in the table. If the query does not return any data it means that data is consistent.

**Important:** you need to replace the following table and column names with relevant names in your database: **foreigntable**, **primarytable**, **foreignkey**, **primarykey**.

```
SELECT 
    fk.foreignkey, 
    COUNT(*) orphaned_rows
FROM foreigntable fk
    LEFT OUTER JOIN primarytable pk
        ON fk.foreignkey = pk.primarykey
WHERE pk.primarykey IS NULL
GROUP BY fk.foreignkey
ORDER BY 2 DESC
```

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/data-profiling-data-quality/60c22cccc7b6e75a19fc02134585b694.png#center "Image title")

## Data profiling tool for SQL Server - Dataedo

If you'd like to get your data profiling efforts to the next level, make the process faster, more convenient, scale it and share results with other people then I recommend you [trying Dataedo](https://dataedo.com/free-trial). Dataedo is a lightweight data discovery and documentation tool with open metadata repository, documentation exports and web data catalog.

It's easy to connect to your database, scan static metadata (tables, columns, keys, etc.) and then perform profiling for specific tables.

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/8d0425145407a98b86edf65e9ac60046.png#center "Image title")

You can save selected profiling data in shared metadata repository (data catalog).

This repository holds additional metadata such as:

1.  data element descriptions and aliases,
2.  table relationships, including physical and manually defined,
3.  data sensitivity classifications,
4.  additional fields and classifications.

This is the best place to write down, and share, the description of tables and columns, assumptions about the data and all the learnings from data profiling exercise.

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/997556e757a845e1487c5fc322d0f5e3.png#center "Image title")

You can also share data profiling in data catalog available for wider audience in on prem web portal - [Dataedo Web Catalog](https://dataedo.com/product/web-catalog).

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/251af7b9cc84efa9e7648c68329f69a4.png#center "Image title")

![Image title](https://dataedo.com/asset/img/markdown/kb/sources/sql-server/data-profiling-in-sql-server/13337c0926046b01dad0d5f79a66595d.png#center "Image title")

[**Try Dataedo for free now**](https://dataedo.com/free-trial)

[**Request a demo**](https://dataedo.com/get-a-demo)