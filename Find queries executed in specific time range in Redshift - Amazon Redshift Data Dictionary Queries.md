Query below shows queries executed in specific time range.

### Notes

Table containing query log retain them approximately for 2-5 days then they are removed.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

Query below lists queries executed between 6 and 12 hours from current time.

```
select usename as username, 
       database, 
       querytxt as query, 
       starttime, 
       endtime, 
       case aborted
            when 1 then 'YES'
            else 'NO'
            end as aborted
from stl_query ql
join svl_user_info us 
     on ql.userid = us.usesysid
where starttime &lt; DATEADD(hour, -6, getdate())
      and starttime &gt; DATEADD(hour, -12, getdate())
order by starttime desc;
```

## Columns

-   **username** - name of user who executed query
-   **database** - name of database in which query was executed
-   **query** - query text
-   **starttime** - start time of a query
-   **endtime** - end time of a query
-   **aborted** - indicate wheter query was aborted

### Rows

-   **One row** represents one query executed in specifiic time range
-   **Scope of rows:** all queries executed in specifiic time range
-   **Ordered by** start time of query

## Sample results

![sample results](https://dataedo.com/asset/img/kb/query/redshift/find-last-query-executed-by-time.png)

### Create beautiful and useful documentation of your Redshift

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)