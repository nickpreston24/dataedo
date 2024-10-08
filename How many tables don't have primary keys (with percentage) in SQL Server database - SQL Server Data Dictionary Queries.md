[This query](https://dataedo.com/kb/query/sql-server/find-tables-without-primary-keys) listed tables without primary keys and this one shows how many of them there are and what is the percentage of total tables.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> 
    all_tabs.[<span>tables</span>] <span>as</span> all_tables,
    no_pk.[<span>tables</span>] <span>as</span> no_pk_tables,
    <span>cast</span>(<span>cast</span>(<span>100.0</span> * no_pk.[<span>tables</span>] / 
    all_tabs.[<span>tables</span>] <span>as</span> <span>decimal</span>(<span>36</span>, <span>1</span>)) <span>as</span> <span>varchar</span>) + <span>'%'</span> <span>as</span> no_pk_percent
<span>from</span>
    (<span>select</span> <span>count</span>(*) <span>as</span> [<span>tables</span>]
    <span>from</span> sys.tables tab
        <span>left</span> <span>outer</span> <span>join</span> sys.indexes pk
            <span>on</span> tab.object_id = pk.object_id 
            <span>and</span> pk.is_primary_key = <span>1</span>
    <span>where</span> pk.object_id <span>is</span> <span>null</span>) <span>as</span> no_pk
<span>inner</span> <span>join</span> 
    (<span>select</span> <span>count</span>(*) <span>as</span> [<span>tables</span>]
    <span>from</span> sys.tables) <span>as</span> all_tabs
<span>on</span> <span>1</span> = <span>1</span>
```

## Columns

-   **all\_tables** - number of all tables in a database
-   **no\_pk\_tables** - number of tables without a primary key
-   **no\_pk\_percent** - percentage of tables without primary key in all tables

## Rows

Query returns just **one row**.

## Sample results

I checked how many tables in Microsoft System Center Configuration Manager database do not have primary keys. It turns out it is only 1.3% which for me is a good result.

![](https://dataedo.com/asset/img/kb/query/sql-server/no_pk_tables_percentage.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)