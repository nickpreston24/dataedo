Query below compares columns (names) in tables between two SQL Server databases. It shows columns missing in either of two databases.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> <span>isnull</span>(db1.table_name, db2.table_name) <span>as</span> [<span>table</span>],
       <span>isnull</span>(db1.column_name, db2.column_name) <span>as</span> [<span>column</span>],
       db1.column_name <span>as</span> database1, 
       db2.column_name <span>as</span> database2
<span>from</span>
(<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> table_name, 
       col.name <span>as</span> column_name
   <span>from</span> [dataedo_6<span>.0</span>].sys.tables <span>as</span> tab
        <span>inner</span> <span>join</span> [dataedo_6<span>.0</span>].sys.columns <span>as</span> <span>col</span>
            <span>on</span> tab.object_id = col.object_id) db1
<span>full</span> <span>outer</span> <span>join</span>
(<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> table_name, 
       col.name <span>as</span> column_name
   <span>from</span> [dataedo_7<span>.0</span>].sys.tables <span>as</span> tab
        <span>inner</span> <span>join</span> [dataedo_7<span>.0</span>].sys.columns <span>as</span> <span>col</span>
            <span>on</span> tab.object_id = col.object_id) db2
<span>on</span> db1.table_name = db2.table_name
<span>and</span> db1.column_name = db2.column_name
<span>where</span> (db1.column_name <span>is</span> <span>null</span> <span>or</span> db2.column_name <span>is</span> <span>null</span>)
<span>order</span> <span>by</span> <span>1</span>, <span>2</span>, <span>3</span>
```

## Instruction

Replace **\[dataedo\_6.0\]** and **\[dataedo\_7.0\]** with names of two of your databases (on SQL Server instance) that you'd like to compare.

## Columns

-   **table** - name of the table with schema
-   **column** - name of column
-   **database1** - if column exists in a table in database 1 then column contains its name (repeats it from column column)
-   **database2** - if column exists in a table in database 2 then column contains its name (repeats it from column column)

## Rows

-   **One row** represents one distinct name of column in specific table.
-   **Scope of rows:** all distinct columns in that exist only in one of the compared databases.
-   **Ordered by** schema, table and column name

## Sample results

I used this query to compare databases of repositories of two versions of Dataedo. It shows that in version 7 we removed **erd\_nodes\_columns** column in **dbo.columns** table and added a number of columns in various tables as you can see below.

![](https://dataedo.com/asset/img/kb/query/sql-server/database_compare_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)