Query below returns distribution of column name lengths (number of characters).

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select len(col.name) as column_name_length,
       count(*) as columns,
       count(distinct tab.object_id) as tables
   from sys.tables as tab
       inner join sys.columns as col 
       on tab.object_id = col.object_id
group by len(col.name)
order by len(col.name)
```

## Columns

-   **column\_name\_length** - lenght in characters of column name
-   **columns** - number of columns with this length
-   **tables** - number of tables that have columns with this name length

## Rows

-   **One row** represents one name lenght (number of characters)
-   **Scope of rows:** each column length that exists in a database
-   **Ordered by** length ascending (from 1 to max)

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/column_length_distribution.png)

If you put it into chart it looks like this:

![](https://dataedo.com/asset/img/kb/query/sql-server/column_length_distribution_chart.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)