This query works on **SQL Server 2016** or newer.

SQL Server 2016 brought new features and table types - **temporal tables** and **external tables**.

Query below lists tables in current database and identifies it's type. Types include:

-   Plain old **Regular table**
-   **System versioned table** (temporal table) (introduced in SQL Server 2016)
-   **History table** (introduced in SQL Server 2016)
-   PolyBase **External table** (introduced in SQL Server 2016)
-   **File table** (introduced in SQL Server 2012)

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(schema_id) as schema_name,
       name as table_name,
        case when is_external = 1 then 'External table'
            when temporal_type = 2 then 'System versioned table'
            when temporal_type = 1 then 'History table'
            when is_filetable = 1 then 'File table'
            else 'Regular table'
        end as table_type
        from sys.tables
order by schema_name, table_name
```

## Columns

-   **schema\_name** - schem name of the table
-   **table\_name** - table name
-   **table\_type** - type of the table identified by the query. Values include:
    -   Regular table
    -   System versioned table
    -   History table
    -   External table
    -   File table

## Rows

-   **One row** represents one table in a database
-   **Scope of rows:** all tables in a database
-   **Ordered by** schema and table name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/table_types_sql_server_2016.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)