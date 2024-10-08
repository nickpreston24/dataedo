Query below lists tables with their triggers.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>],
    trig.name <span>as</span> trigger_name,
    <span>case</span> <span>when</span> is_instead_of_trigger = <span>1</span> <span>then</span> <span>'Instead of'</span>
        <span>else</span> <span>'After'</span> <span>end</span> <span>as</span> [activation],
    (<span>case</span> <span>when</span> objectproperty(trig.object_id, <span>'ExecIsUpdateTrigger'</span>) = <span>1</span> 
            <span>then</span> <span>'Update '</span> <span>else</span> <span>''</span> <span>end</span>
    + <span>case</span> <span>when</span> objectproperty(trig.object_id, <span>'ExecIsDeleteTrigger'</span>) = <span>1</span> 
            <span>then</span> <span>'Delete '</span> <span>else</span> <span>''</span> <span>end</span>
    + <span>case</span> <span>when</span> objectproperty(trig.object_id, <span>'ExecIsInsertTrigger'</span>) = <span>1</span> 
            <span>then</span> <span>'Insert '</span> <span>else</span> <span>''</span> <span>end</span>
    ) <span>as</span> [<span>event</span>],
    <span>case</span> <span>when</span> trig.[<span>type</span>] = <span>'TA'</span> <span>then</span> <span>'Assembly (CLR) trigger'</span>
        <span>when</span> trig.[<span>type</span>] = <span>'TR'</span> <span>then</span> <span>'SQL trigger'</span> 
        <span>else</span> <span>''</span> <span>end</span> <span>as</span> [<span>type</span>],
    <span>case</span> <span>when</span> is_disabled = <span>1</span> <span>then</span> <span>'Disabled'</span>
        <span>else</span> <span>'Active'</span> <span>end</span> <span>as</span> [<span>status</span>],
    object_definition(trig.object_id) <span>as</span> [definition]
<span>from</span> sys.triggers trig
    <span>inner</span> <span>join</span> sys.objects tab
        <span>on</span> trig.parent_id = tab.object_id
<span>order</span> <span>by</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name, trig.name;
```

## Columns

-   **table** - name of the trigger table (for table triggers) with schema name
-   **trigger\_name** - name of the trigger
-   **activation** - trigger activation time: _After_ or _Instead of_
-   **event** - specific SQL operation: _Insert_, _Update_ or _Delete_
-   **type** - object type:
    -   Assembly (CLR) trigger
    -   SQL trigger
-   **status** - trigger status
    -   Active
    -   Disabled
-   **definition** - SQL definiton of trigger

## Rows

-   **One row** represents one trigger
-   **Scope of rows:** all column, table triggers in a database
-   **Ordered by** schema and table name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/triggers_by_table.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)