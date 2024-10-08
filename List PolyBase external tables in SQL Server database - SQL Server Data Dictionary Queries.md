This query works on **SQL Server 2016** or newer.

SQL Server 2016 introduced new feature - **PolyBase**, that enables you to run queries on external data in **Hadoop** or to import/export data from **Azure Blob Storage**. External data is accesible through external tables.

Query below returns external PolyBase tables.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select 
    schema_name(schema_id) as schema_name,
    t.name as table_name,
    s.name as source_name,
    s.location, 
    s.type_desc as source_type,
    f.name as format_name,
    f.format_type,
    f.field_terminator,
    f.string_delimiter,
    f.row_terminator,
    f.encoding,
    f.data_compression
from sys.external_tables t
    inner join sys.external_data_sources s
        on t.data_source_id = s.data_source_id
    inner join sys.external_file_formats f
        on t.file_format_id = f.file_format_id
order by schema_name, table_name
```

## Columns

-   **schema\_name** - external table schema name
-   **table\_name** - external table name
-   **source\_name** - name of the data source in the current database
-   **location** - the connection string, which includes the protocol, IP address, and port for the external data source
-   **source\_type** - data source type:
    -   HADOOP
    -   RDBMS
    -   SHARD\_MAP\_MANAGER
    -   RemoteDataArchiveTypeExtDataSource
-   **format\_name** - date file format name
-   **format\_type** - file format type
    -   DELIMITEDTEXT
    -   RCFILE
    -   ORC
    -   PARQUET
-   **field\_terminator** - field terminator (for format\_type = DELIMITEDTEXT)
-   **string\_delimiter** - string delimiter (for format\_type = DELIMITEDTEXT)
-   **row\_terminator** - character string that terminates each row in the external Hadoop file (for format\_type = DELIMITEDTEXT)
-   **encoding** - encoding method for the external Hadoop file (for format\_type = DELIMITEDTEXT)
-   **data\_compression** - data compression method

## Rows

-   **One row** represents one external table
-   **Scope of rows:** only external tables are included
-   **Ordered by** schema and table name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/polybase_external_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)