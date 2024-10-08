This query returns information on operating system SQL Server database runs on.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> host_platform <span>as</span> os_type,
       host_distribution <span>as</span> os,
       host_release <span>as</span> <span>version</span>
<span>from</span> sys.dm_os_host_info;
```

## Columns

-   **os\_type** - type of operating system: Windows or Linux
-   **os** - operating system description
-   **version** - version of operating system. For windows check list of values to find out version of OS.

## Windows values

| Value | OS Version |
| --- | --- |
| 10.0\* | Windows 10, Windows Server 2019, Windows Server 2016 |
| 6.3\* | Windows 8.1, Windows Server 2012 R2 |
| 6.2 | Windows 8, Windows Server 2012 |
| 6.1 | Windows 7, Windows Server 2008 R2 |
| 6.0 | Windows Server 2008, Windows Vista |
| 5.2 | Windows Server 2003 R2, Windows Server 2003, Windows XP 64-Bit Edition |
| 5.1 | Windows XP |
| 5.0 | Windows 2000 |

## Rows

Query returns just **one row**

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/os_version.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)