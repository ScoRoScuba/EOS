namespace EOS2.Data.Migrations.ELMAHLogging
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Diagnostics.CodeAnalysis;

    public partial class Initial : DbMigration
    {
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1013:ClosingCurlyBracketsMustBeSpacedCorrectly", Justification = "This is generated code, suppress all style errors."),
        SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1026:CodeMustNotContainSpaceAfterNewKeywordInImplicitlyTypedArrayAllocation", Justification = "This is generated code, suppress all style errors."),
        SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1012:OpeningCurlyBracketsMustBeSpacedCorrectly", Justification = "This is generated code, suppress all style errors."),
        SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1024:ColonsMustBeSpacedCorrectly", Justification = "This is generated code, suppress all style errors."),
        SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1011:ClosingSquareBracketsMustBeSpacedCorrectly", Justification = "This is generated code, suppress all style errors."),
        SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:OpeningSquareBracketsMustBeSpacedCorrectly", Justification = "This is generated code, suppress all style errors."),
        SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1116:SplitParametersMustStartOnLineAfterDeclaration", Justification = "This is generated code, suppress all style errors."),
        SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "This is generated code, suppress all style errors."),
        SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "This is generated code, suppress all style errors.")]
        public override void Up()
        {
            CreateTable(
                "dbo.ELMAH_Error",
                c => new
                    {
                        ErrorId = c.Guid(nullable: false, identity: true),
                        Application = c.String(nullable: false, maxLength: 60),
                        Host = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 100),
                        Source = c.String(nullable: false, maxLength: 60),
                        Message = c.String(nullable: false, maxLength: 500),
                        User = c.String(nullable: false, maxLength: 50),
                        StatusCode = c.Int(nullable: false),
                        TimeUtc = c.DateTime(nullable: false),
                        Sequence = c.Int(nullable: false, identity: true),
                        AllXml = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.ErrorId);

            CreateIndex(
                "dbo.ELMAH_Error", 
                new []{"Application", "TimeUtc", "Sequence"}, 
                true, 
                "IX_ELMAH_Error_App_Time_Seq", 
                false, 
                null);

            CreateStoredProcedure(
                "dbo.ELMAH_GetErrorXml",
                p => new
                         {
                                 Application = p.String(60),
                                 ErrorId = p.Guid()
                             },
                             body : @"SET NOCOUNT ON

                                    SELECT [AllXml]
                                    FROM  [ELMAH_Error]
                                    WHERE [ErrorId] = @ErrorId
                                    AND [Application] = @Application");

            CreateStoredProcedure(
                "dbo.ELMAH_GetErrorsXml",
                p => new
                         {
                            Application = p.String(60),
                            PageIndex = p.Int(0),
                            PageSize  = p.Int(15),
                            TotalCount = p.Int(outParameter:true)
                         },
                         body : @"
                                SET NOCOUNT ON

                                DECLARE @FirstTimeUTC DATETIME
                                DECLARE @FirstSequence INT
                                DECLARE @StartRow INT
                                DECLARE @StartRowIndex INT

                                SELECT 
                                    @TotalCount = COUNT(1) 
                                FROM 
                                    [ELMAH_Error]
                                WHERE 
                                    [Application] = @Application

                                -- Get the ID of the first error for the requested page

                                SET @StartRowIndex = @PageIndex * @PageSize + 1

                                IF @StartRowIndex <= @TotalCount
                                BEGIN

                                    SET ROWCOUNT @StartRowIndex

                                    SELECT  
                                        @FirstTimeUTC = [TimeUtc],
                                        @FirstSequence = [Sequence]
                                    FROM 
                                        [ELMAH_Error]
                                    WHERE   
                                        [Application] = @Application
                                    ORDER BY 
                                        [TimeUtc] DESC, 
                                        [Sequence] DESC

                                END
                                ELSE
                                BEGIN

                                    SET @PageSize = 0

                                END

                                -- Now set the row count to the requested page size and get
                                -- all records below it for the pertaining application.

                                SET ROWCOUNT @PageSize

                                SELECT 
                                    errorId     = [ErrorId], 
                                    application = [Application],
                                    host        = [Host], 
                                    type        = [Type],
                                    source      = [Source],
                                    message     = [Message],
                                    [user]      = [User],
                                    statusCode  = [StatusCode], 
                                    time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
                                FROM 
                                    [ELMAH_Error] error
                                WHERE
                                    [Application] = @Application
                                AND
                                    [TimeUtc] <= @FirstTimeUTC
                                AND 
                                    [Sequence] <= @FirstSequence
                                ORDER BY
                                    [TimeUtc] DESC, 
                                    [Sequence] DESC
                                FOR
                                    XML AUTO");

            CreateStoredProcedure("dbo.ELMAH_LogError",
                p => new
                         {
                             ErrorId = p.Guid(),
                             Application = p.String(60),
                             Host = p.String(30),
                             Type = p.String(100),
                             Source = p.String(60),
                             Message = p.String(500),
                             User = p.String(50),
                             AllXml = p.String(storeType:"NTEXT"),
                            StatusCode = p.Int(),
                            TimeUtc = p.DateTime()
                         },
                         body: @"SET NOCOUNT ON
                                INSERT
                                INTO
                                    [ELMAH_Error]
                                    (
                                        [ErrorId],
                                        [Application],
                                        [Host],
                                        [Type],
                                        [Source],
                                        [Message],
                                        [User],
                                        [AllXml],
                                        [StatusCode],
                                        [TimeUtc]
                                    )
                                VALUES
                                    (
                                        @ErrorId,
                                        @Application,
                                        @Host,
                                        @Type,
                                        @Source,
                                        @Message,
                                        @User,
                                        @AllXml,
                                        @StatusCode,
                                        @TimeUtc
                                    )");
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.ELMAH_GetErrorXml");
            DropStoredProcedure("dbo.ELMAH_GetErrorsXml");
            DropStoredProcedure("dbo.ELMAH_LogError");
            DropIndex("dbo.ELMAH_Error", "IX_ELMAH_Error_App_Time_Seq");
            DropTable("dbo.ELMAH_Error");
        }
    }
}
