This query lets you find out most popular column names.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> col.name <span>as</span> column_name,
      <span>count</span>(*) <span>as</span> <span>tables</span>,
      <span>cast</span>(<span>100.0</span> * <span>count</span>(*) / 
      (<span>select</span> <span>count</span>(*) <span>from</span> sys.tables) <span>as</span> <span>numeric</span>(<span>36</span>, <span>1</span>)) <span>as</span> percent_tables
   <span>from</span> sys.tables <span>as</span> tab
       <span>inner</span> <span>join</span> sys.columns <span>as</span> <span>col</span> 
       <span>on</span> tab.object_id = col.object_id
<span>group</span> <span>by</span> col.name 
<span>having</span> <span>count</span>(*) &gt; <span>1</span>
<span>order</span> <span>by</span> <span>count</span>(*) <span>desc</span>
```

## Columns

-   **column\_name** - column name
-   **tables** - number of tables that have particular column name
-   **percent\_tables** - percentage of tables with column with that name

## Rows

-   **One row** represents particular column name (each name appears only once)
-   **Scope of rows:** all colum names that appear at least twice (in two tables)
-   **Ordered by** the most popular first

## Sample results

Below is a list of most popular column names in AdventureWorks database. As you can see **ModifiedDate** column exists in 70 tables, which is almost all of them (95.8%).

![](https://dataedo.com/asset/img/kb/query/sql-server/column_names_most_used.png)

Similarly, in **Dataedo** repository database most popular columns are creation and modifiaction time stamps and logins.

You can learn more about typical metadata fields here: [6 Typical Metadata Fields Stored by Applications](https://dataedo.com/blog/typical-metadata-fields-stroed-by-applications)

![](https://dataedo.com/asset/img/kb/query/sql-server/column_names_most_used_dataedo.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)