Applies to: **Dataedo 24.x (current)** versions, Article available also for: [10.x](https://dataedo.com/docs-10/list-tables-that-do-not-appear-in-any-er-diagram), [23.x](https://dataedo.com/docs-23/list-tables-that-do-not-appear-in-any-er-diagram)

This query lists tables that are not added to any ER diagram:

```
SELECT d.title as [database], t.[schema], t.[name] as table_name
FROM [dbo].[tables] t
    inner join [dbo].[databases] d
        on t.database_id = d.database_id
where t.table_id not in (select table_id from [dbo].[erd_nodes])
 and t.object_type in ('TABLE') -- add 'VIEWS' for views
 order by d.title, t.[schema], t.[name]
```

This query returns tables assigned to modules but not added to an ER diagram:

```
SELECT d.title as [database], 
        m.title as [module_name], 
        t.[schema], 
        t.[name] as table_name
FROM [dbo].[tables] t
    inner join [dbo].[tables_modules] tm
        on t.table_id = tm.table_id
    inner join [dbo].modules m
        on m.module_id = tm.module_id
    inner join [dbo].[databases] d
        on t.database_id = d.database_id
where t.table_id not in (select table_id 
                           from [dbo].[erd_nodes] en 
                          where en.module_id = tm.module_id)
and t.object_type in ('TABLE')
order by d.title, m.title, t.[schema], t.[name]
```