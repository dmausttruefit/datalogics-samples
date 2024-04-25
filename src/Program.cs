using System.CommandLine;
using Datalogics.PDFL;

var rootCommand = new RootCommand("Sample app exhibiting Datalogics features");

var outputOption = new Option<string>(
    name: "--output",
    description: "The desired destination to save the PDF document, including file name",
    getDefaultValue: () => "keywords.pdf"
);

var setKeywordsCommand = new Command("--set-keywords", "Set Keywords on a newly created PDF")
{
    outputOption
};

rootCommand.AddCommand(setKeywordsCommand);

setKeywordsCommand.SetHandler(CreateDocumentWithKeywords, outputOption);

return rootCommand.InvokeAsync(args).Result;

static async Task CreateDocumentWithKeywords(string outputPath)
{
    await using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite);
    using var library = new Library();
    using var document = new Document();
    document.CreatePage(Document.BeforeFirstPage, new Rect(0, 0, 612, 792));
    document.Keywords = "International;Institutional";
    document.Save(SaveFlags.Full, fileStream);
}
