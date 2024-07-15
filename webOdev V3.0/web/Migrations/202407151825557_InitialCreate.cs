namespace web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BlogFoto = c.Binary(),
                        BlogBaslik = c.String(),
                        BlogIcerik = c.String(),
                        BlogTarih = c.DateTime(),
                        Tarih = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Duyurus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DuyuruFoto = c.Binary(),
                        DuyuruBaslik = c.String(),
                        DuyuruIcerik = c.String(),
                        DuyuruTarih = c.DateTime(),
                        Tarih = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Moduls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModulFoto = c.Binary(),
                        ModulBaslik = c.String(),
                        ModulIcerik = c.String(),
                        Tarih = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Oneris",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(),
                        Telefon = c.String(),
                        Eposta = c.String(),
                        Mesaj = c.String(),
                        Tarih = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Referans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReferansFoto = c.Binary(),
                        ReferansBaslik = c.String(),
                        ReferansIcerik = c.String(),
                        ReferansTarih = c.DateTime(),
                        Tarih = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SliderFoto = c.Binary(),
                        SliderText = c.String(),
                        BaslangicTarih = c.DateTime(),
                        BitisTarih = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Takims",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Foto = c.Binary(),
                        AdSoyad = c.String(),
                        Icerik = c.String(),
                        Tip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LoginViewModels",
                c => new
                    {
                        KullaniciID = c.Int(nullable: false, identity: true),
                        KullaniciAdi = c.String(nullable: false),
                        Roles = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.KullaniciID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginViewModels");
            DropTable("dbo.Takims");
            DropTable("dbo.Sliders");
            DropTable("dbo.Referans");
            DropTable("dbo.Oneris");
            DropTable("dbo.Moduls");
            DropTable("dbo.Duyurus");
            DropTable("dbo.Blogs");
        }
    }
}
