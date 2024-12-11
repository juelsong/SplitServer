namespace ImportExportTest
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Infrastructure.Entity;
    using ESys.UnitTest;
    using ESys.Utilty.Defs;
    using DocumentFormat.OpenXml.EMMA;
    using Furion.DatabaseAccessor;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NPOI.SS.Formula.Functions;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    // ������ʱ����ƥ��ԭʼJSON�ṹ
    public class TempRoot
    {
        public string Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public string Timestamp { get; set; } // ��Ϊ�ַ������գ�Ȼ��ת��ΪDateTime
    }

    // ����Record��
    public class Record
    {
        public string Type { get; set; }
        public string OrganismGenus { get; set; }
        public string Species { get; set; }
        public string Description { get; set; }
        public string Strain { get; set; }
        public string OrganismSeverity { get; set; }
        public string SeverityLocation { get; set; }
        public int RowNumber { get; set; }
        public int InsertStatus { get; set; }
        public string ErrorDescription { get; set; }
    }

    // ����Root��
    public class Root<T>
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
    }
    public class NpoiMemoryStream : MemoryStream
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AllowClose { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NpoiMemoryStream()
        {
            this.AllowClose = true;
        }

        public override void Close()
        {
            if (this.AllowClose)
            {
                base.Close();
            }
        }
    }


    [TestClass]
    public class ImportTest
    {
        private readonly string organism = "����";
        private readonly string location = "����";
        private readonly string site = "������";
        private readonly string equipment = "�����豸";
        private readonly string media = "�Լ��Ĳ�";
        private readonly string[] locationHeaders = new string[] {
                "* ��������",
                "* ����",
                "* ����",
                "* �ྻ����",
                "* �ϼ�����",
                "�ڶ��ྻ����",
                "����"
        };
        private readonly string[] organismHeaders = new string[] {
                "* �����",
                "* ����",
                "* ����",
                "* ����",
                "����",
                "�²���",
                "�²�������"
        };
        private readonly string[] siteHeaders = new string[] {
                "* ����������",
                "* ����",
                "* ����",
                "* �ྻ����",
                "* ��������",
                "�ڶ��ྻ����",
                "����"
        };
        private readonly string[] equipmentHeaders = new string[] {
                "* �豸����",
                "* ����",
                "����",
                "����",
                "��λ",
                "����",
                "У׼����",
                "�´�У׼����",
                "У׼ֵ",
                "���Ʊ��",
                "���к�"
        };
        private readonly string[] mediaHeaders = new string[] {
                "* �Ĳ�����",
                "* ����",
                "* ����",
                "* ��Ч��",
                "����",
                "����",
                "��Ӧ��",
                "��Ӧ������",
                "������״̬",
                "���������"
        };


        private IServiceProvider serviceProvider = null;
        private IServiceScope scope = null;
        private IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository = null;



        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            UnitTestContext.Instance.Initialize<ESys.App.Startup>("importexportsettings.json");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            UnitTestContext.Instance.Cleanup();
        }
        private IWorkbook workbook;


        [TestInitialize]
        public void TestInitialize()
        {
            this.scope = Furion.App.RootServices.CreateScope();
            this.serviceProvider = this.scope.ServiceProvider;
            this.serviceProvider.GetService<ITenantService>().SetTenantScope("Emis");
            this.msRepository = this.serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();


            this.workbook = new XSSFWorkbook();
            this.workbook.CreateSheet(this.organism);
            this.workbook.CreateSheet(this.location);
            this.workbook.CreateSheet(this.site);
            this.workbook.CreateSheet(this.equipment);
            this.workbook.CreateSheet(this.media);
        }
        public static IFormFile ConvertStreamToFormFile(Stream stream, string fileName)
        {
            //var memoryStream = new MemoryStream();
            //stream.CopyTo(memoryStream);
            stream.Position = 0;
            return new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };
        }

        [TestMethod]
        public async Task TestSuccessImport()
        {
            using var client = UnitTestContext.Instance.GetAdminClient();
            Task<HttpResponseMessage> ImportExcel(string type, bool keepName, Stream stream)
            {
                var str = JsonSerializer.Serialize(new { }, UnitTestContext.Instance.DefaultJsonSerializerOptions);
                //var content = new StringContent(str, Encoding.UTF8, "application/json");
                //var fileContent = new StreamContent(stream);
                //using var content = new MultipartFormDataContent();
                //fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                //fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                //{
                //    Name = "file",
                //    FileName = "ImportFile"
                //};

                //// ���ļ���ӵ� MultipartFormDataContent
                //content.Add(fileContent, "file", "ImportFile");
                //// �����������
                ////content.Add(new StringContent(type), "type");
                //content.Add(new StringContent(keepName.ToString()), "keepName");

                var formFile = ConvertStreamToFormFile(stream, "ImportFile.xlsx");
                var content = new MultipartFormDataContent
                {
                    { new StreamContent(formFile.OpenReadStream()), "file", formFile.FileName },
                    { new StringContent(keepName.ToString()), "keepName" }
                };

                return client.PostAsync($"/ImportAndExport/importExcel/{type}", content);
            }
            async Task AssertSucess(HttpResponseMessage rsp)
            {
                Assert.IsNotNull(rsp);
                Assert.AreEqual(rsp.StatusCode, System.Net.HttpStatusCode.OK);
                var str = await rsp.Content.ReadAsStringAsync();
                var ret = JsonSerializer.Deserialize<Result>(str);
                Assert.IsTrue(ret.Success);
            }

            async Task TestOrganism()
            {
                var sheet = this.workbook.GetSheet(this.organism);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.organismHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.organismHeaders[i]);
                }
                for (var i = 1; i < 5; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    row.CreateCell(0).SetCellValue($"Type");
                    row.CreateCell(1).SetCellValue(this.msRepository.Slave1<OrganismGenus>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Description);
                    row.CreateCell(2).SetCellValue($"TestName_{i}");
                    row.CreateCell(3).SetCellValue($"Description");
                    row.CreateCell(4).SetCellValue($"Strain");
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Organism),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess(rsp);
            }
            async Task TestLocation()
            {
                var sheet = this.workbook.GetSheet(this.location);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.locationHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.locationHeaders[i]);
                }
                for (var i = 1; i < 5; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    row.CreateCell(0).SetCellValue($"Name_{i}");
                    row.CreateCell(1).SetCellValue($"Description");
                    row.CreateCell(2).SetCellValue(this.msRepository.Slave1<LocationType>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    row.CreateCell(3).SetCellValue(this.msRepository.Slave1<Classification>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    row.CreateCell(4).SetCellValue(this.msRepository.Slave1<Location>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Location),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess(rsp);
            }
            async Task TestSite()
            {
                var sheet = this.workbook.GetSheet(this.site);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.siteHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.siteHeaders[i]);
                }
                for (var i = 1; i < 5; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    row.CreateCell(0).SetCellValue($"Name_{i}");
                    row.CreateCell(1).SetCellValue($"Description");
                    row.CreateCell(2).SetCellValue(this.msRepository.Slave1<SiteType>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    row.CreateCell(3).SetCellValue(this.msRepository.Slave1<Classification>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    row.CreateCell(4).SetCellValue(this.msRepository.Slave1<Location>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Site),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess(rsp);
            }
            async Task TestEquipment()
            {
                var sheet = this.workbook.GetSheet(this.equipment);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.equipmentHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.equipmentHeaders[i]);
                }
                for (var i = 1; i < 5; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    row.CreateCell(0).SetCellValue(this.msRepository.Slave1<EquipmentType>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Description);
                    row.CreateCell(1).SetCellValue($"Name_{i}");
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Equipment),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess(rsp);
            }
            async Task TestMedia()
            {
                var sheet = this.workbook.GetSheet(this.media);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.mediaHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.mediaHeaders[i]);
                }
                for (var i = 1; i < 5; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    row.CreateCell(0).SetCellValue(this.msRepository.Slave1<MediaType>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    row.CreateCell(1).SetCellValue($"Name_{i}");
                    row.CreateCell(2).SetCellValue($"LotNumber_{i}");
                    row.CreateCell(3).SetCellValue($"{DateTime.Now.ToString("yyyy/MM/dd")}");
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Media),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess(rsp);
            }

            await TestOrganism();
            await TestLocation();
            await TestSite();
            await TestEquipment();
            await TestMedia();

        }
        [TestMethod]
        public async Task TestErrorImport()
        {
            using var client = UnitTestContext.Instance.GetAdminClient();
            Task<HttpResponseMessage> ImportExcel(string type, bool keepName, Stream stream)
            {
                var str = JsonSerializer.Serialize(new { }, UnitTestContext.Instance.DefaultJsonSerializerOptions);
                var formFile = ConvertStreamToFormFile(stream, "ImportFile.xlsx");
                var content = new MultipartFormDataContent
                {
                    { new StreamContent(formFile.OpenReadStream()), "file", formFile.FileName },
                    { new StringContent(keepName.ToString()), "keepName" }
                };

                return client.PostAsync($"/ImportAndExport/importExcel/{type}", content);
            }
            async Task AssertSucess<T>(HttpResponseMessage rsp, int errorCount)
            {
                Assert.IsNotNull(rsp);
                Assert.AreEqual(rsp.StatusCode, System.Net.HttpStatusCode.OK);
                var str = await rsp.Content.ReadAsStringAsync();

                //var ret = JsonSerializer.Deserialize<Result>(str);
                var ret = JsonSerializer.Deserialize<Result<string>>(str);
                var data = JsonSerializer.Deserialize<List<T>>(ret.Data);
                Assert.AreEqual(errorCount, data.Count);

            }

            async Task TestOrganism()
            {
                var sheet = this.workbook.GetSheet(this.organism);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.organismHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.organismHeaders[i]);
                }
                for (var i = 1; i < 6; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    if (i != 1)
                        row.CreateCell(0).SetCellValue($"Type");
                    if (i != 2)
                        row.CreateCell(1).SetCellValue(this.msRepository.Slave1<OrganismGenus>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Description);
                    if (i != 3)
                        row.CreateCell(2).SetCellValue($"TestName_{i}");

                    if (i != 4)
                        row.CreateCell(3).SetCellValue($"Description");

                    if (i != 5)

                        row.CreateCell(4).SetCellValue($"Strain");
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Organism),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess<ESys.ImportExport.Model.Organism>(rsp, 4);
            }
            async Task TestLocation()
            {
                var sheet = this.workbook.GetSheet(this.location);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.locationHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.locationHeaders[i]);
                }
                for (var i = 1; i < 5; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    if (i != 1)

                        row.CreateCell(0).SetCellValue($"Name_{i}");
                    if (i != 2)

                        row.CreateCell(1).SetCellValue($"Description");
                    if (i != 3)
                        row.CreateCell(2).SetCellValue(this.msRepository.Slave1<LocationType>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    if (i != 4)
                        row.CreateCell(3).SetCellValue(this.msRepository.Slave1<Classification>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    if (i != 5)
                        row.CreateCell(4).SetCellValue(this.msRepository.Slave1<Location>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Location),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess<ESys.ImportExport.Model.Location>(rsp, 3);
            }
            async Task TestSite()
            {
                var sheet = this.workbook.GetSheet(this.site);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.siteHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.siteHeaders[i]);
                }
                for (var i = 1; i < 5; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    if (i != 1)
                        row.CreateCell(0).SetCellValue($"Name_{i}");
                    if (i != 2)
                        row.CreateCell(1).SetCellValue($"Description");
                    if (i != 3)
                        row.CreateCell(2).SetCellValue(this.msRepository.Slave1<SiteType>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    if (i != 4)
                        row.CreateCell(3).SetCellValue(this.msRepository.Slave1<Classification>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    if (i != 5)
                        row.CreateCell(4).SetCellValue(this.msRepository.Slave1<Location>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Site),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess<ESys.ImportExport.Model.Site>(rsp, 3);
            }
            async Task TestEquipment()
            {
                var sheet = this.workbook.GetSheet(this.equipment);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.equipmentHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.equipmentHeaders[i]);
                }
                for (var i = 1; i < 5; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    if (i != 1)
                        row.CreateCell(0).SetCellValue(this.msRepository.Slave1<EquipmentType>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Description);
                    if (i != 2)
                        row.CreateCell(1).SetCellValue($"Name_{i}");
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Equipment),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess<ESys.ImportExport.Model.Equipment>(rsp, 2);
            }
            async Task TestMedia()
            {
                var sheet = this.workbook.GetSheet(this.media);
                sheet.CreateRow(0);
                var header = sheet.GetRow(0);
                for (var i = 0; i < this.mediaHeaders.Length; i++)
                {
                    header.CreateCell(i).SetCellValue(this.mediaHeaders[i]);
                }
                for (var i = 1; i < 5; i++)
                {
                    sheet.CreateRow(i);
                    var row = sheet.GetRow(i);
                    if (i != 1)
                        row.CreateCell(0).SetCellValue(this.msRepository.Slave1<MediaType>()
                                                    .FirstOrDefault(i => i.IsActive)
                                                    .Name);
                    if (i != 2)
                        row.CreateCell(1).SetCellValue($"Name_{i}");
                    if (i != 3)
                        row.CreateCell(2).SetCellValue($"LotNumber_{i}");
                    if (i != 4)
                        row.CreateCell(3).SetCellValue($"{DateTime.Now.ToString("yyyy/MM/dd")}");
                }

                using var ms = new NpoiMemoryStream();
                ms.AllowClose = false; //�Ƚ�ֹClose
                this.workbook.Write(ms);
                var rsp = await ImportExcel(nameof(Media),
                    false,
                    ms
                );
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin); //��������ǰλ�ã������沽��ڶ���ʹ��
                ms.AllowClose = true; //��������Close�������ִ�н��������MemoryStream���Ա������رջ���
                await AssertSucess<ESys.ImportExport.Model.Media>(rsp, 4);
            }

            await TestOrganism();
            await TestLocation();
            await TestSite();
            await TestEquipment();
            await TestMedia();

        }

        //[TestMethod]
        //public void Testsrt()
        //{
        //    Assert.IsTrue(1 > 0);

        //}
    }
}