Query below compares columns (names) in tables between two SQL Server schemas. It shows columns missing in either of two schemas.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select isnull(schema1.table_name, schema2.table_name) as [table],
       isnull(schema1.column_name, schema2.column_name) as [column],
       schema1.column_name as database1,
       schema2.column_name as database2
from
(select tab.name as table_name, 
        col.name as column_name
 from sys.tables as tab
 join sys.columns as col
      on tab.object_id = col.object_id
 where schema_name(tab.schema_id) = 'schema_1') schema1
full join
(select tab.name as table_name, 
        col.name as column_name
 from sys.tables as tab
 join sys.columns as col
      on tab.object_id = col.object_id
 where schema_name(tab.schema_id) = 'schema_2') schema2
on schema1.table_name = schema2.table_name
and schema1.column_name = schema2.column_name
where (schema1.column_name is null or schema2.column_name is null)
order by 1, 2, 3
```

## Instruction

Replace **schema\_1** and **schema\_2** with names of two of your schemas (in SQL Server database) that you'd like to compare.

## Columns

-   **table** - name of the table
-   **column** - name of column
-   **schema1** - if column exists in a table in schema 1 then column contains its name (repeats it from column column)
-   **schema2** - if column exists in a table in schema 2 then column contains its name (repeats it from column column)

## Rows

-   **One row** represents one distinct name of column in specific table.
-   **Scope of rows:** all distinct columns in that exist only in one of the compared databases.
-   **Ordered by** schema, table and column name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/schema_compare_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)