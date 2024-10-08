Query below returns data types used in a database ordered by the number of their occurance.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> t.name <span>as</span> data_type,
    <span>count</span>(*) <span>as</span> [<span>columns</span>],
    <span>cast</span>(<span>100.0</span> * <span>count</span>(*) /
    (<span>select</span> <span>count</span>(*) <span>from</span> sys.tables <span>as</span> tab <span>inner</span> <span>join</span>
        sys.columns <span>as</span> <span>col</span> <span>on</span> tab.object_id = col.object_id)
            <span>as</span> <span>numeric</span>(<span>36</span>, <span>1</span>)) <span>as</span> percent_columns,
      <span>count</span>(<span>distinct</span> tab.object_id) <span>as</span> [<span>tables</span>],
      <span>cast</span>(<span>100.0</span> * <span>count</span>(<span>distinct</span> tab.object_id) /
      (<span>select</span> <span>count</span>(*) <span>from</span> sys.tables) <span>as</span> <span>numeric</span>(<span>36</span>, <span>1</span>)) <span>as</span> percent_tables
  <span>from</span> sys.tables <span>as</span> tab
       <span>inner</span> <span>join</span> sys.columns <span>as</span> <span>col</span>
        <span>on</span> tab.object_id = col.object_id
       <span>left</span> <span>join</span> sys.types <span>as</span> t
        <span>on</span> col.user_type_id = t.user_type_id
<span>group</span> <span>by</span> t.name
<span>order</span> <span>by</span> <span>count</span>(*) <span>desc</span>
```

## Columns

-   **data\_type** - built in or user data type without length or precision, e.g. int, varchar or date
-   **columns** - number of columns in a database with this data type
-   **percent\_columns** - percentage of columns with this data type. Rows add up to 100%
-   **tables** - number of tables in a database with this data type
-   **percent\_tables** - percentage of tables with columns with this data type.

## Rows

-   **One row** represents one data type
-   **Scope of rows:** all data types used in a database
-   **Ordered by** number of columns descending

## Sample results

Those results show data types used in **AdventureWorks** database. As you can see most popular data type is **int** - it is used in 137 columns which is 27.9% of all columns in 62 tables (which 86.1% of all tables).

![](https://dataedo.com/asset/img/kb/query/sql-server/most_popular_data_type.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)