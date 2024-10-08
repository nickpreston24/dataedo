The query below lists all columns with XML data types in SQL Server database.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.name <span>as</span> [<span>table</span>],
       c.column_id,
       c.name <span>as</span> column_name,
       type_name(user_type_id) <span>as</span> data_type,
       is_xml_document
<span>from</span> sys.columns c
<span>join</span> sys.tables t
     <span>on</span> t.object_id = c.object_id
<span>where</span> type_name(user_type_id) <span>in</span> (<span>'xml'</span>)
<span>order</span> <span>by</span> [<span>table</span>],
         c.column_id;
```

## Columns

-   **table** - name of the table with schema name
-   **column\_id** - column position in table
-   **column\_name** - name of the column
-   **data\_type** - type of data
-   **is\_xml\_dcoument** -
    -   1 - content is a complete XML document with only one root element
    -   0 - content is a document fragment

## Rows

-   **One row** represents one column with a XML data type
-   **Scope of rows:** all columns containing XML data types in the database
-   **Ordered by** schema name, table name and column position in table

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/xml_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)