Query below compares columns (names) in tables between two SQL Server schemas. It shows columns missing in either of two schemas.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> <span>isnull</span>(schema1.table_name, schema2.table_name) <span>as</span> [<span>table</span>],
       <span>isnull</span>(schema1.column_name, schema2.column_name) <span>as</span> [<span>column</span>],
       schema1.column_name <span>as</span> database1,
       schema2.column_name <span>as</span> database2
<span>from</span>
(<span>select</span> tab.name <span>as</span> table_name, 
        col.name <span>as</span> column_name
 <span>from</span> sys.tables <span>as</span> tab
 <span>join</span> sys.columns <span>as</span> <span>col</span>
      <span>on</span> tab.object_id = col.object_id
 <span>where</span> schema_name(tab.schema_id) = <span>'schema_1'</span>) schema1
<span>full</span> <span>join</span>
(<span>select</span> tab.name <span>as</span> table_name, 
        col.name <span>as</span> column_name
 <span>from</span> sys.tables <span>as</span> tab
 <span>join</span> sys.columns <span>as</span> <span>col</span>
      <span>on</span> tab.object_id = col.object_id
 <span>where</span> schema_name(tab.schema_id) = <span>'schema_2'</span>) schema2
<span>on</span> schema1.table_name = schema2.table_name
<span>and</span> schema1.column_name = schema2.column_name
<span>where</span> (schema1.column_name <span>is</span> <span>null</span> <span>or</span> schema2.column_name <span>is</span> <span>null</span>)
<span>order</span> <span>by</span> <span>1</span>, <span>2</span>, <span>3</span>
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