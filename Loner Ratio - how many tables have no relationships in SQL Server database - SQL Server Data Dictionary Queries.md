[This query](https://dataedo.com/kb/query/sql-server/tables-without-foreign-keys) listed tables that have no foreign keys, meaning they are not referencing any table or are not on the "many" side of FK.

Query below lists something a little different - tables that are not referencing and are not referenced by other tables. Something I called [**Loner Tables**](https://dataedo.com/blog/test-if-your-database-has-foreign-keys). This diagram illustrates the concept:

![](https://dataedo.com/asset/img/blog/loner_table.png)

[Learn more about Loner Tables](https://dataedo.com/blog/test-if-your-database-has-foreign-keys)

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> <span>count</span>(*) [table_count],
    <span>sum</span>(<span>case</span> <span>when</span> fks.cnt + refs.cnt = <span>0</span> <span>then</span> <span>1</span> <span>else</span> <span>0</span> <span>end</span>) 
    <span>as</span> [loner_tables],
    <span>cast</span>(<span>cast</span>(<span>100.0</span> * <span>sum</span>(<span>case</span> <span>when</span> fks.cnt + refs.cnt = <span>0</span> <span>then</span> <span>1</span> <span>else</span> <span>0</span> <span>end</span>) 
    / <span>count</span>(*) <span>as</span> <span>decimal</span>(<span>36</span>, <span>1</span>)) <span>as</span> <span>varchar</span>) + <span>'%'</span> <span>as</span> [loner_ratio]
<span>from</span> (<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> tab,
        <span>count</span>(fk.name) cnt
    <span>from</span> sys.tables <span>as</span> tab
        <span>left</span> <span>join</span> sys.foreign_keys <span>as</span> fk
            <span>on</span> tab.object_id = fk.parent_object_id
    <span>group</span> <span>by</span> schema_name(tab.schema_id), tab.name) fks
    <span>inner</span> <span>join</span> 
    (<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> tab,
        <span>count</span>(fk.name) cnt
    <span>from</span> sys.tables <span>as</span> tab
        <span>left</span> <span>join</span> sys.foreign_keys <span>as</span> fk
            <span>on</span> tab.object_id = fk.referenced_object_id
    <span>group</span> <span>by</span> schema_name(tab.schema_id), tab.name) refs
    <span>on</span> fks.tab = refs.tab
```

## Columns

-   **table\_count** - number of tables in database
-   **loner\_tables** - number of [Loner Tables](https://dataedo.com/blog/test-if-your-database-has-foreign-keys) in the database
-   **loner\_ratio** - [Loner Ratio](https://dataedo.com/blog/test-if-your-database-has-foreign-keys) - % of Loner Tables in the database

## Rows

-   **Scope of rows:** query returns one row

## Sample results

Loner Ratio in AdventureWorks sample database is 6.9%, meaning that 6.9% of tables are not related by foreign keys to any other table.

![](https://dataedo.com/asset/img/kb/query/sql-server/loner_ratio.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)