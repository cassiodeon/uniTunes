namespace UnitunesMvc.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Autor_Id = c.Int(),
                        Genero_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autor", t => t.Autor_Id)
                .ForeignKey("dbo.Categoria", t => t.Genero_Id)
                .Index(t => t.Autor_Id)
                .Index(t => t.Genero_Id);
            
            CreateTable(
                "dbo.Autor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Livro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroPaginas = c.Long(nullable: false),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(nullable: false),
                        Preco = c.Double(nullable: false),
                        Imagem = c.Binary(),
                        Conteudo = c.Binary(),
                        Categoria_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.Categoria_Id)
                .Index(t => t.Categoria_Id);
            
            CreateTable(
                "dbo.Streaming",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duracao = c.Long(nullable: false),
                        Tipo = c.Int(nullable: false),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(nullable: false),
                        Preco = c.Double(nullable: false),
                        Imagem = c.Binary(),
                        Conteudo = c.Binary(),
                        Categoria_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.Categoria_Id)
                .Index(t => t.Categoria_Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrimeiroNome = c.String(nullable: false, maxLength: 20),
                        UltimoNome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Tipo = c.Int(nullable: false),
                        Senha = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Streaming", "Categoria_Id", "dbo.Categoria");
            DropForeignKey("dbo.Livro", "Categoria_Id", "dbo.Categoria");
            DropForeignKey("dbo.Album", "Genero_Id", "dbo.Categoria");
            DropForeignKey("dbo.Album", "Autor_Id", "dbo.Autor");
            DropIndex("dbo.Streaming", new[] { "Categoria_Id" });
            DropIndex("dbo.Livro", new[] { "Categoria_Id" });
            DropIndex("dbo.Album", new[] { "Genero_Id" });
            DropIndex("dbo.Album", new[] { "Autor_Id" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Streaming");
            DropTable("dbo.Livro");
            DropTable("dbo.Categoria");
            DropTable("dbo.Autor");
            DropTable("dbo.Album");
        }
    }
}
