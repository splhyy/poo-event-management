using EventManagement.Domain.Entities;

namespace EventManagement.Console;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("=== 🎯 SISTEMA DE GERENCIAMENTO DE EVENTOS ===\n");

        #region Região 1: Speaker Examples
        System.Console.WriteLine("🎤 REGIÃO 1: EXEMPLOS DE PALESTRANTES\n");

        // Criar palestrantes válidos
        System.Console.WriteLine("1. Criando palestrantes válidos:");
        try
        {
            var speaker1 = new Speaker(1, "João Silva", "joao.silva@email.com");
            speaker1.SetBiography("Especialista em C# com 10 anos de experiência");
            speaker1.Company = "Microsoft";
            speaker1.LinkedInProfile = "https://linkedin.com/in/joaosilva";
            
            System.Console.WriteLine($"   ✅ {speaker1}");

            var speaker2 = new Speaker(2, "Maria Santos", "maria.santos@tech.com");
            speaker2.SetBiography("Arquiteta de Software e consultora em DevOps");
            System.Console.WriteLine($"   ✅ {speaker2}");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"   ❌ Erro: {ex.Message}");
        }

        // Tentar criar com dados inválidos
        System.Console.WriteLine("\n2. Tentando criar palestrantes com dados inválidos:");
        
        try
        {
            var invalidSpeaker = new Speaker(0, "Nome Válido", "email@valido.com");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"   ❌ SpeakerId 0: {ex.Message}");
        }

        try
        {
            var invalidSpeaker = new Speaker(3, "   ", "email@valido.com");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"   ❌ Nome vazio: {ex.Message}");
        }

        try
        {
            var invalidSpeaker = new Speaker(4, "Nome Válido", "email-invalido");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"   ❌ Email inválido: {ex.Message}");
        }

        // Demonstrar SetBiography
        System.Console.WriteLine("\n3. Demonstrando SetBiography:");
        var speaker3 = new Speaker(5, "Carlos Oliveira", "carlos@email.com");
        
        speaker3.SetBiography("   Biografia com espaços   ");
        System.Console.WriteLine($"   ✅ Biografia com espaços: '{speaker3.Biography}'");
        
        speaker3.SetBiography(null);
        System.Console.WriteLine($"   ✅ Biografia com null: '{speaker3.Biography}'");
        
        speaker3.SetBiography("");
        System.Console.WriteLine($"   ✅ Biografia vazia: '{speaker3.Biography}'");

        // Mostrar Company e LinkedInProfile com null
        System.Console.WriteLine("\n4. Propriedades com [AllowNull]:");
        var speaker4 = new Speaker(6, "Ana Costa", "ana@email.com");
        
        speaker4.Company = null;
        speaker4.LinkedInProfile = null;
        
        System.Console.WriteLine($"   ✅ Company com null: '{speaker4.Company}'");
        System.Console.WriteLine($"   ✅ LinkedInProfile com null: '{speaker4.LinkedInProfile}'");
        #endregion

        #region Região 2: Venue Examples
        System.Console.WriteLine("\n\n🏛️ REGIÃO 2: EXEMPLOS DE LOCAIS\n");

        // Criar locais válidos
        System.Console.WriteLine("1. Criando locais válidos:");
        try
        {
            var venue1 = new Venue(1, "Centro de Convenções", "Av. Principal, 1000", 500);
            venue1.SetDescription("Moderno centro com infraestrutura completa");
            venue1.ParkingInfo = "Estacionamento subterrâneo com 200 vagas";
            
            System.Console.WriteLine($"   ✅ {venue1}");
            System.Console.WriteLine($"      Descrição: {venue1.Description}");
            System.Console.WriteLine($"      Estacionamento: {venue1.ParkingInfo}");

            var venue2 = new Venue(2, "Hotel Premium", "Rua Secundária, 500", 200);
            System.Console.WriteLine($"   ✅ {venue2}");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"   ❌ Erro: {ex.Message}");
        }

        // Demonstrar Venue.Default
        System.Console.WriteLine("\n2. Demonstrando Venue.Default:");
        var defaultVenue = Venue.Default;
        System.Console.WriteLine($"   ✅ Local padrão: {defaultVenue}");

        // Mostrar SetDescription
        System.Console.WriteLine("\n3. Demonstrando SetDescription:");
        var venue3 = new Venue(3, "Auditório Central", "Praça Central, 50", 150);
        
        venue3.SetDescription("   Descrição com espaços   ");
        System.Console.WriteLine($"   ✅ Descrição com espaços: '{venue3.Description}'");
        
        venue3.SetDescription(null);
        System.Console.WriteLine($"   ✅ Descrição com null: '{venue3.Description}'");

        // Demonstrar ParkingInfo com null
        System.Console.WriteLine("\n4. ParkingInfo com [AllowNull]:");
        var venue4 = new Venue(4, "Sala de Reuniões", "Alameda das Flores, 200", 50);
        
        venue4.ParkingInfo = null;
        System.Console.WriteLine($"   ✅ ParkingInfo com null: '{venue4.ParkingInfo}'");
        #endregion

        #region Região 3: Event Examples
        System.Console.WriteLine("\n\n🎭 REGIÃO 3: EXEMPLOS DE EVENTOS\n");

        // Criar eventos válidos
        System.Console.WriteLine("1. Criando eventos válidos:");
        try
        {
            var futureDate = DateTime.Now.AddMonths(2);
            var event1 = new Event(1, "Conferência .NET 2025", futureDate, TimeSpan.FromHours(8));
            event1.SetEventCode("DOTNET2025");
            event1.SetDescription("Maior conferência sobre tecnologias .NET da América Latina");
            
            System.Console.WriteLine($"   ✅ {event1}");
            System.Console.WriteLine($"      Código: {event1.EventCode}");
            System.Console.WriteLine($"      Descrição: {event1.Description}");

            var event2 = new Event(2, "Workshop DevOps", futureDate.AddDays(7), TimeSpan.FromHours(4));
            System.Console.WriteLine($"   ✅ {event2}");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"   ❌ Erro: {ex.Message}");
        }

        // Mostrar lazy loading de Venue
        System.Console.WriteLine("\n2. Demonstrando Lazy Loading do Venue:");
        var futureDate2 = DateTime.Now.AddMonths(1);
        var event3 = new Event(3, "Meetup Cloud Computing", futureDate2, TimeSpan.FromHours(3));
        
        System.Console.WriteLine($"   ✅ Venue carregado sob demanda: {event3.Venue}");

        // Demonstrar SetEventCode e [DisallowNull]
        System.Console.WriteLine("\n3. Demonstrando SetEventCode com [DisallowNull]:");
        var event4 = new Event(4, "Seminário AI", futureDate2.AddDays(14), TimeSpan.FromHours(6));
        
        event4.SetEventCode("AI2025");
        System.Console.WriteLine($"   ✅ EventCode definido: {event4.EventCode}");
        
        try
        {
            event4.SetEventCode(null!);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"   ❌ EventCode com null: {ex.Message}");
        }

        // Mostrar Requirements e Notes com [AllowNull]
        System.Console.WriteLine("\n4. Requirements e Notes com [AllowNull]:");
        var event5 = new Event(5, "Hackathon", futureDate2.AddDays(30), TimeSpan.FromHours(24));
        
        event5.Requirements = null;
        event5.Notes = "   Observações com espaços   ";
        
        System.Console.WriteLine($"   ✅ Requirements com null: '{event5.Requirements}'");
        System.Console.WriteLine($"   ✅ Notes com espaços: '{event5.Notes}'");
        #endregion

        #region Região 4: Complete Scenario
        System.Console.WriteLine("\n\n🎯 REGIÃO 4: CENÁRIO COMPLETO\n");

        // Criar palestrante
        var speaker = new Speaker(10, "Dr. Sofia Fernandes", "sofia.fernandes@tech.com");
        speaker.SetBiography("PhD em Ciência da Computação com 15 anos de experiência");
        speaker.Company = "Tech Research Institute";

        // Criar local
        var venue = new Venue(10, "Centro de Inovação", "Rua da Tecnologia, 500", 300);
        venue.SetDescription("Espaço moderno para eventos tecnológicos");

        // Criar evento
        var evento = new Event(10, "Conferência de Inovação 2025", DateTime.Now.AddMonths(3), TimeSpan.FromHours(6));
        evento.SetEventCode("INNOV2025");
        evento.SetDescription("Evento anual sobre as últimas tendências em tecnologia");
        evento.AssignMainSpeaker(speaker);
        evento.Requirements = "Conhecimento básico em programação";
        evento.Notes = "Trazer notebook para os workshops";

        // Exibir informações
        System.Console.WriteLine("📋 DETALHES DO EVENTO:");
        System.Console.WriteLine($"   {evento}");
        System.Console.WriteLine($"   📍 Local: {evento.Venue}");
        System.Console.WriteLine($"   🎤 Palestrante Principal: {evento.MainSpeaker?.FullName ?? "A definir"}");
        System.Console.WriteLine($"   📝 Requisitos: {evento.Requirements}");
        System.Console.WriteLine($"   📌 Observações: {evento.Notes}");
        #endregion

        System.Console.WriteLine("\n=== 🎉 DEMONSTRAÇÃO CONCLUÍDA ===");
    }
}