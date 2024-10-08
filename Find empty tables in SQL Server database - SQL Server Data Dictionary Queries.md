This query returns list of tables in a database without any rows.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>]
   <span>from</span> sys.tables tab
        <span>inner</span> <span>join</span> sys.partitions part
            <span>on</span> tab.object_id = part.object_id
<span>where</span> part.index_id <span>IN</span> (<span>1</span>, <span>0</span>) <span>-- 0 - table without PK, 1 table with PK</span>
<span>group</span> <span>by</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name
<span>having</span> <span>sum</span>(part.rows) = <span>0</span>
<span>order</span> <span>by</span> [<span>table</span>]
```

## Columns

-   **table** - table name with schema name

## Rows

-   **One row** represents one table
-   **Scope of rows:** only empty tables (without rows)
-   **Ordered by** schema and table name

## Sample results

Empty tables in our SSRS repository:

![](https://dataedo.com/asset/img/kb/query/sql-server/empty_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)