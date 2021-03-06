USE [master]
GO
/****** Object:  Database [sistema_checador]    Script Date: 09/10/2018 18:13:31 ******/
CREATE DATABASE [sistema_checador]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sistema_checador', FILENAME = N'C:\BD_checador\sistema_checador.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'sistema_checador_log', FILENAME = N'C:\BD_checador\sistema_checador_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [sistema_checador] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sistema_checador].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [sistema_checador] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [sistema_checador] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [sistema_checador] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [sistema_checador] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [sistema_checador] SET ARITHABORT OFF 
GO
ALTER DATABASE [sistema_checador] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [sistema_checador] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [sistema_checador] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [sistema_checador] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [sistema_checador] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [sistema_checador] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [sistema_checador] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [sistema_checador] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [sistema_checador] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [sistema_checador] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [sistema_checador] SET  DISABLE_BROKER 
GO
ALTER DATABASE [sistema_checador] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [sistema_checador] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [sistema_checador] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [sistema_checador] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [sistema_checador] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [sistema_checador] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [sistema_checador] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [sistema_checador] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [sistema_checador] SET  MULTI_USER 
GO
ALTER DATABASE [sistema_checador] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [sistema_checador] SET DB_CHAINING OFF 
GO
ALTER DATABASE [sistema_checador] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [sistema_checador] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [sistema_checador]
GO
/****** Object:  Table [dbo].[checador]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[checador](
	[id_checador] [int] NOT NULL,
	[ip] [nvarchar](20) NOT NULL,
	[puerto] [nvarchar](20) NOT NULL,
	[id_sucursal] [int] NULL,
	[estatus] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_chekdor] PRIMARY KEY CLUSTERED 
(
	[id_checador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[empleado]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empleado](
	[id_empleado] [int] NOT NULL,
	[nombre] [nvarchar](40) NOT NULL,
	[apellido_pat] [nvarchar](30) NOT NULL,
	[apellido_mat] [nvarchar](50) NOT NULL,
	[departamento] [nvarchar](50) NULL,
	[id_privilegio] [int] NOT NULL,
	[telefono] [nvarchar](20) NULL,
	[calle] [nvarchar](50) NULL,
	[num_ext] [nvarchar](50) NULL,
	[num_int] [nvarchar](50) NULL,
	[colonia] [nvarchar](50) NULL,
	[codigo_postal] [nvarchar](50) NULL,
	[poblacion] [nvarchar](50) NULL,
	[municipio] [nvarchar](50) NULL,
	[estado] [nvarchar](50) NULL,
	[pais] [nvarchar](50) NULL,
	[puesto] [nvarchar](50) NULL,
	[RFC] [nvarchar](20) NULL,
	[CURP] [nvarchar](20) NULL,
	[estatus] [nvarchar](9) NOT NULL,
	[fecha_alta] [datetime] NOT NULL,
	[observaciones] [nvarchar](300) NULL,
	[email] [nvarchar](50) NULL,
	[fecha_baja] [datetime] NULL,
	[NSS] [nvarchar](50) NULL,
	[tipo_contrato] [nvarchar](50) NULL,
	[sueldo_diario] [decimal](18, 2) NULL,
	[sueldo_diario_integrado] [decimal](18, 2) NULL,
	[sueldo_base_quincenal] [decimal](18, 2) NULL,
	[tipo_salario] [nvarchar](10) NULL,
	[dias_aguinaldo] [int] NULL,
	[dias_vacaciones] [int] NULL,
	[riesgo_puesto] [nvarchar](50) NULL,
	[periodicidad_pago] [nvarchar](50) NULL,
	[banco] [nvarchar](50) NULL,
	[cuenta_bancaria] [nvarchar](20) NULL,
	[tarjeta_despensa] [nvarchar](50) NULL,
	[clave_edenred] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[horas_extra] [int] NULL,
	[retardos] [int] NULL,
	[total_min_retardo] [decimal](18, 0) NULL,
	[id_horario] [int] NOT NULL,
 CONSTRAINT [PK_empleado] PRIMARY KEY CLUSTERED 
(
	[id_empleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[empleado_sucursal]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empleado_sucursal](
	[id_empleado] [int] NOT NULL,
	[id_sucursal] [int] NOT NULL,
	[fecha_entrada] [datetime] NOT NULL,
	[fecha_salida] [datetime] NULL,
 CONSTRAINT [PK_empleado_sucursal] PRIMARY KEY CLUSTERED 
(
	[id_empleado] ASC,
	[id_sucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[empresa]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empresa](
	[id_empresa] [int] NOT NULL,
	[nombre_empresa] [nvarchar](40) NOT NULL,
	[direccion] [nvarchar](50) NOT NULL,
	[ciudad] [nvarchar](50) NOT NULL,
	[estado] [nvarchar](50) NOT NULL,
	[pais] [nvarchar](50) NOT NULL,
	[cp] [nvarchar](6) NOT NULL,
	[telefono] [nvarchar](13) NOT NULL,
	[email] [nvarchar](30) NULL,
	[sitio_web] [nvarchar](50) NULL,
 CONSTRAINT [PK_empresa] PRIMARY KEY CLUSTERED 
(
	[id_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[horarios]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[horarios](
	[id_horario] [int] NOT NULL,
	[horario] [nvarchar](50) NOT NULL,
	[hr_entrada] [time](5) NOT NULL,
	[hr_salida] [time](5) NOT NULL,
	[horas_diarias] [int] NOT NULL,
	[lunes] [int] NOT NULL,
	[martes] [int] NOT NULL,
	[miercoles] [int] NOT NULL,
	[jueves] [int] NOT NULL,
	[viernes] [int] NOT NULL,
	[sabado] [int] NOT NULL,
	[domingo] [int] NOT NULL,
	[horas_totales_quincenales] [int] NOT NULL,
	[hr_salida_descanso] [time](5) NULL,
	[hr_entrada_descanso] [time](5) NULL,
	[tolerancia] [int] NOT NULL,
 CONSTRAINT [PK_horarios] PRIMARY KEY CLUSTERED 
(
	[id_horario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[privilegios]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[privilegios](
	[id_privilegio] [int] NOT NULL,
	[nombre] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_privilegios] PRIMARY KEY CLUSTERED 
(
	[id_privilegio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[registros]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[registros](
	[id_checador] [int] NOT NULL,
	[id_empleado] [int] NOT NULL,
	[id_sucursal] [int] NOT NULL,
	[fecha_entrada] [datetime] NULL,
	[fecha_salida] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sucursal]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sucursal](
	[id_sucursal] [int] NOT NULL,
	[nombre] [nvarchar](50) NOT NULL,
	[calle] [nvarchar](50) NOT NULL,
	[colonia] [nvarchar](50) NOT NULL,
	[num_ext] [nvarchar](50) NOT NULL,
	[num_int] [nvarchar](50) NULL,
	[codigo_postal] [nvarchar](50) NOT NULL,
	[poblacion] [nvarchar](50) NOT NULL,
	[municipio] [nvarchar](50) NOT NULL,
	[estado] [nvarchar](50) NOT NULL,
	[pais] [nvarchar](50) NOT NULL,
	[telefono] [nvarchar](15) NOT NULL,
	[estatus] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_sucursal] PRIMARY KEY CLUSTERED 
(
	[id_sucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[usuario]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[usuario] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[Vista_Checador]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vista_Checador]
AS
SELECT        dbo.checador.id_checador, dbo.sucursal.nombre AS sucursal, dbo.checador.ip, dbo.checador.puerto, dbo.checador.estatus
FROM            dbo.checador INNER JOIN
                         dbo.sucursal ON dbo.checador.id_sucursal = dbo.sucursal.id_sucursal

GO
/****** Object:  View [dbo].[Vista_Empleados]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vista_Empleados]
AS
SELECT        dbo.empleado.id_empleado, dbo.empleado.nombre + ' ' + dbo.empleado.apellido_pat + ' ' + dbo.empleado.apellido_mat AS nombre_completo, dbo.empleado.CURP, dbo.empleado.RFC, dbo.empleado.NSS, 
                         dbo.sucursal.nombre AS sucursal, dbo.empleado.departamento, dbo.horarios.horario, dbo.empleado.telefono, 
                         dbo.empleado.calle + N' #' + dbo.empleado.num_ext + N', Col. ' + dbo.empleado.colonia + N' CP: ' + dbo.empleado.codigo_postal AS direccion, dbo.empleado.num_int, 
                         dbo.empleado.poblacion + N', ' + dbo.empleado.municipio + N', ' + dbo.empleado.estado + N', ' + dbo.empleado.pais AS Localizacion, dbo.empleado.puesto, dbo.empleado.email, dbo.empleado.fecha_alta, 
                         dbo.empleado.fecha_baja, dbo.empleado.observaciones, dbo.empleado.tipo_contrato, dbo.empleado.sueldo_diario, dbo.empleado.sueldo_diario_integrado, dbo.empleado.sueldo_base_quincenal, dbo.empleado.tipo_salario, 
                         dbo.empleado.dias_aguinaldo, dbo.empleado.dias_vacaciones, dbo.empleado.riesgo_puesto, dbo.empleado.periodicidad_pago, dbo.empleado.banco, dbo.empleado.cuenta_bancaria, dbo.empleado.tarjeta_despensa, 
                         dbo.empleado.clave_edenred, dbo.empleado.password, dbo.empleado.horas_extra, dbo.empleado.estatus
FROM            dbo.empleado INNER JOIN
                         dbo.empleado_sucursal ON dbo.empleado.id_empleado = dbo.empleado_sucursal.id_empleado INNER JOIN
                         dbo.sucursal ON dbo.empleado_sucursal.id_sucursal = dbo.sucursal.id_sucursal AND dbo.empleado_sucursal.fecha_salida IS NULL INNER JOIN
                         dbo.horarios ON dbo.empleado.id_horario = dbo.horarios.id_horario

GO
/****** Object:  View [dbo].[Vista_Horario]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vista_Horario]
AS
SELECT        id_horario, horario, hr_entrada, hr_salida_descanso, hr_entrada_descanso, hr_salida, tolerancia, horas_diarias, horas_totales_quincenales, lunes, martes, miercoles, jueves, viernes, sabado, domingo
FROM            dbo.horarios

GO
/****** Object:  View [dbo].[Vista_Sucursal]    Script Date: 09/10/2018 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vista_Sucursal]
AS
SELECT        id_sucursal, nombre, calle + N' #' + num_ext + N', Col. ' + colonia + N' CP: ' + codigo_postal AS direccion, num_int, poblacion + N', ' + municipio + N', ' + estado + N', ' + pais AS localizacion, telefono, estatus
FROM            dbo.sucursal

GO
ALTER TABLE [dbo].[checador]  WITH CHECK ADD  CONSTRAINT [FK_checador_sucursal1] FOREIGN KEY([id_sucursal])
REFERENCES [dbo].[sucursal] ([id_sucursal])
GO
ALTER TABLE [dbo].[checador] CHECK CONSTRAINT [FK_checador_sucursal1]
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_horarios] FOREIGN KEY([id_horario])
REFERENCES [dbo].[horarios] ([id_horario])
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_horarios]
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_privilegios] FOREIGN KEY([id_privilegio])
REFERENCES [dbo].[privilegios] ([id_privilegio])
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_privilegios]
GO
ALTER TABLE [dbo].[empleado_sucursal]  WITH CHECK ADD  CONSTRAINT [FK_empleado_sucursal_empleado] FOREIGN KEY([id_empleado])
REFERENCES [dbo].[empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[empleado_sucursal] CHECK CONSTRAINT [FK_empleado_sucursal_empleado]
GO
ALTER TABLE [dbo].[empleado_sucursal]  WITH CHECK ADD  CONSTRAINT [FK_empleado_sucursal_sucursal] FOREIGN KEY([id_sucursal])
REFERENCES [dbo].[sucursal] ([id_sucursal])
GO
ALTER TABLE [dbo].[empleado_sucursal] CHECK CONSTRAINT [FK_empleado_sucursal_sucursal]
GO
ALTER TABLE [dbo].[registros]  WITH CHECK ADD  CONSTRAINT [FK_checador_empleado] FOREIGN KEY([id_empleado])
REFERENCES [dbo].[empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[registros] CHECK CONSTRAINT [FK_checador_empleado]
GO
ALTER TABLE [dbo].[registros]  WITH CHECK ADD  CONSTRAINT [FK_checador_sucursal] FOREIGN KEY([id_sucursal])
REFERENCES [dbo].[sucursal] ([id_sucursal])
GO
ALTER TABLE [dbo].[registros] CHECK CONSTRAINT [FK_checador_sucursal]
GO
ALTER TABLE [dbo].[registros]  WITH CHECK ADD  CONSTRAINT [FK_registros_checador] FOREIGN KEY([id_checador])
REFERENCES [dbo].[checador] ([id_checador])
GO
ALTER TABLE [dbo].[registros] CHECK CONSTRAINT [FK_registros_checador]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "checador"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "sucursal"
            Begin Extent = 
               Top = 6
               Left = 285
               Bottom = 136
               Right = 494
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Checador'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Checador'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[37] 4[50] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -718
      End
      Begin Tables = 
         Begin Table = "empleado"
            Begin Extent = 
               Top = 0
               Left = 52
               Bottom = 154
               Right = 267
            End
            DisplayFlags = 280
            TopColumn = 10
         End
         Begin Table = "empleado_sucursal"
            Begin Extent = 
               Top = 6
               Left = 291
               Bottom = 136
               Right = 500
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sucursal"
            Begin Extent = 
               Top = 138
               Left = 302
               Bottom = 268
               Right = 511
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "horarios"
            Begin Extent = 
               Top = 156
               Left = 38
               Bottom = 286
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 40
         Width = 284
         Width = 1500
         Width = 3060
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 3570
         Width = 1500
         Width = 4620
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Empleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 6330
         Alias = 2325
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Empleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Empleados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "horarios"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 211
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 8
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Horario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Horario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[25] 2[6] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "sucursal"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 9
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 3285
         Width = 1500
         Width = 4665
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1845
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Sucursal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Sucursal'
GO
USE [master]
GO
ALTER DATABASE [sistema_checador] SET  READ_WRITE 
GO
