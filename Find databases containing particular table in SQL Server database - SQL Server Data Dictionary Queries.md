Query below finds all databases in SQL Server instance containing particular table (table name must include schema name). In this case this table is **dbo.version**.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> [<span>name</span>] <span>as</span> [database_name] <span>from</span> sys.databases 
<span>where</span> 
    <span>case</span> <span>when</span> state_desc = <span>'ONLINE'</span> 
        <span>then</span> object_id(<span>quotename</span>([<span>name</span>]) + <span>'.[dbo].[version]'</span>, <span>'U'</span>) 
    <span>end</span> <span>is</span> <span>not</span> <span>null</span>
<span>order</span> <span>by</span> <span>1</span>
```

## Columns

-   **database\_name** - database names

## Rows

-   **One row** represents one database
-   **Scope of rows:** all databases containing provided table schama and name
-   **Ordered by** database name

## Sample results

All databases containing **dbo.version** table - that's how if search for Dataedo repositories.

![](https://dataedo.com/asset/img/kb/query/sql-server/databases_with_table.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)