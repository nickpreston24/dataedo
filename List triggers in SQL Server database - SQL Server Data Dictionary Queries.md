Query below lists triggers in a database with their details.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select trg.name as trigger_name,
    schema_name(tab.schema_id) + '.' + tab.name as [table],
    case when is_instead_of_trigger = 1 then 'Instead of'
        else 'After' end as [activation],
    (case when objectproperty(trg.object_id, 'ExecIsUpdateTrigger') = 1
                then 'Update ' else '' end 
    + case when objectproperty(trg.object_id, 'ExecIsDeleteTrigger') = 1
                then 'Delete ' else '' end
    + case when objectproperty(trg.object_id, 'ExecIsInsertTrigger') = 1
                then 'Insert' else '' end
    ) as [event],
    case when trg.parent_class = 1 then 'Table trigger'
        when trg.parent_class = 0 then 'Database trigger' 
    end [class], 
    case when trg.[type] = 'TA' then 'Assembly (CLR) trigger'
        when trg.[type] = 'TR' then 'SQL trigger' 
        else '' end as [type],
    case when is_disabled = 1 then 'Disabled'
        else 'Active' end as [status],
    object_definition(trg.object_id) as [definition]
from sys.triggers trg
    left join sys.objects tab
        on trg.parent_id = tab.object_id
order by trg.name;
```

## Columns

-   **trigger\_name** - name of the trigger
-   **table** - name of the trigger table (for table triggers) with schema name
-   **activation** - trigger activation time: _After_ or _Instead of_
-   **event** - specific SQL operation: _Insert_, _Update_ or _Delete_
-   **class** - trigger class:
    -   Database trigger - for the DDL triggers
    -   Table trigger - for object or column for the DML triggers
-   **type** - object type:
    -   Assembly (CLR) trigger
    -   SQL trigger
-   **status** - trigger status
    -   Active
    -   Disabled
-   **definition** - SQL definiton of trigger

## Rows

-   **One row** represents one trigger
-   **Scope of rows:** all column, table and database (DDL) triggers in a database
-   **Ordered by** trigger name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/triggers.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)