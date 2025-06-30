using BLL.Interfaces;
using DAL.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;

namespace BLL.Services
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> GenerateRentalAgreementAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content()
                        .Column(column =>
                        {
                            column.Item().Text("ДОГОВОР АРЕНДЫ АВТОМОБИЛЯ").Bold().FontSize(16).Underline().AlignCenter();
                            column.Item().Text($"Номер брони: {booking.Id}");
                            column.Item().Text($"Пользователь ID: {booking.UserId}");
                            column.Item().Text($"Автомобиль ID: {booking.CarId}");
                            column.Item().Text($"Дата начала аренды: {booking.StartDate:dd.MM.yyyy}");
                            column.Item().Text($"Дата окончания аренды: {booking.EndDate:dd.MM.yyyy}");
                            column.Item().Text($"Дата создания: {booking.CreatedAt:dd.MM.yyyy HH:mm}");
                            column.Item().Text($"Место получения: {booking.PickupLocation}");
                            column.Item().Text($"Место возврата: {booking.DropoffLocation}");
                            column.Item().Text($"Тип страховки: {booking.InsuranceType ?? "не выбрана"}");
                            column.Item().Text($"Доп. опции: {(booking.Extras?.Count > 0 ? string.Join(", ", booking.Extras) : "нет")}");
                            column.Item().Text($"Итоговая сумма: {booking.TotalPrice} BYN");
                            column.Item().PaddingTop(15).Text("Договор считается заключённым при подтверждении бронирования.").Italic();
                        });
                });
            });


            var pdfBytes = document.GeneratePdf();

            var filePath = Path.Combine("wwwroot", "contracts", $"contract_{booking.Id}.pdf");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            await File.WriteAllBytesAsync(filePath, pdfBytes, cancellationToken);

            return pdfBytes; ;
        }
    }
}
