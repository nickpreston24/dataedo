Query below compares columns (names) in tables between two SQL Server databases. It shows columns missing in either of two databases.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select isnull(db1.table_name, db2.table_name) as [table],
       isnull(db1.column_name, db2.column_name) as [column],
       db1.column_name as database1, 
       db2.column_name as database2
from
(select schema_name(tab.schema_id) + '.' + tab.name as table_name, 
       col.name as column_name
   from [dataedo_6.0].sys.tables as tab
        inner join [dataedo_6.0].sys.columns as col
            on tab.object_id = col.object_id) db1
full outer join
(select schema_name(tab.schema_id) + '.' + tab.name as table_name, 
       col.name as column_name
   from [dataedo_7.0].sys.tables as tab
        inner join [dataedo_7.0].sys.columns as col
            on tab.object_id = col.object_id) db2
on db1.table_name = db2.table_name
and db1.column_name = db2.column_name
where (db1.column_name is null or db2.column_name is null)
order by 1, 2, 3
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