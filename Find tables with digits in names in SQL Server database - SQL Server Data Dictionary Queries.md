Tables usually do not have digits in their names, and if they do they have a special meaning. We use them to name backup tables with date of an backup (of single specific table while sensitive operation), archival tables with year or for partitioning tables.

Query below finds all tables with digits in their names.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> schema_name(t.schema_id) <span>as</span> schema_name,
       t.name <span>as</span> table_name
<span>from</span> sys.tables t
<span>where</span> t.name <span>like</span> <span>'%[0-9]%'</span>
<span>order</span> <span>by</span> schema_name,
         table_name;
```

## Columns

-   **schema\_name** - name of schema table was found in
-   **table\_name** - name of found table

## Rows

-   **One row** represents a table
-   **Scope of rows:** all found tables
-   **Ordered by** schema name, table name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/find_tables_with_digits.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)