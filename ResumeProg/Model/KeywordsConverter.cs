using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ResumeProg.ViewModel;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace ResumeProg.Model
{
    public class KeywordsConverter
    {

        #region Const

        public const char START_WORD_KEY = '<';
        public const char END_WORD_KEY = '>';

        public const char START_LIST_KEY = '[';
        public const char END_LIST_KEY = ']';

        public const char START_ITEM_KEY = '{';
        public const char END_ITEM_KEY = '}';

        public const char ESCAPE_KEY = '\\';
        public static readonly Regex ESCAPE_REGEX = new Regex(@"(\\).");

        #endregion
        private Info Info { get; set; }
        private IDocument Document { get; set; }

        public KeywordsConverter(Info info, IDocument document)
        {
            Info = info;
            Document = document;
        }

        public async void ConvertDocumentAsync(string savePath)
        {
            string documentString = GetAllDocumentString(Document);
            List<string> keywords = (await GetAllKeywordsAsync(documentString)).ToList();
            List<List<Paragraph>> itemKeys = GetItemKeyParagraphs(Document);
            string[] listsNames = keywords.Where(x => x.StartsWith(START_LIST_KEY.ToString()) && x.EndsWith(END_LIST_KEY.ToString())).ToArray();

            //Duplicvating paragraphs of lists
            #region ListsPart
            for (int i = 0; i < listsNames.Length; i++)
            {
                var list = (Info[listsNames[i], null] as IEnumerable<IPropertyName>).ToList();
                List<string> listProperties = keywords.Skip(keywords.ToList().IndexOf(listsNames[i]) + 1).ToList();
                listProperties = listProperties.Take(listProperties.IndexOf(END_ITEM_KEY.ToString())).ToList();
                CloneItemKey(listsNames[i], itemKeys[i], listProperties.ToArray(), list);
            }
            #endregion

            //Remove used properties
            bool delete = false;
            for (int i = 0; i < keywords.Count; )
            {
                if (keywords[i].StartsWith(START_LIST_KEY.ToString()))
                    delete = true;
                if (delete)
                {
                    keywords.Remove(keywords[i]);
                    continue;
                }
                if (keywords[i].StartsWith(END_ITEM_KEY.ToString()))
                    delete = false;
                if (keywords[i].StartsWith(START_WORD_KEY.ToString())) i++;
            }

            //Inserting properties of main document
            foreach(string property in keywords)
            {
                Document.Replace(property, Info[property.Substring(1, property.Length - 2)], false, true);
            }

            //Changing first founded image {now it doesnt work}
            switch ("")
            {
                case "1":
                    break;
                default:
                    foreach (Section sec in Document.Sections)
                    {
                        foreach (Paragraph paragraph in sec.Paragraphs)
                        {
                            //Loop through the child elements of paragraph
                            foreach (DocumentObject docObj in paragraph.ChildObjects)
                            {
                                if (docObj.DocumentObjectType == DocumentObjectType.Picture)
                                {
                                    DocPicture picture = docObj as DocPicture;
                                    picture.LoadImage(VM.Instance.Info.Photo);
                                    goto case "1";
                                }
                            }
                        }
                    }
                    break;
            }


            //Garbadge removing

            foreach (Section section in Document.Sections)
            {
                foreach (Paragraph paragraph in section.Paragraphs)
                {
                    foreach(string it in listsNames)
                    {
                        if (paragraph.Text.Contains(it))
                        {
                            section.Paragraphs.Remove(paragraph);
                            break;
                        }
                    }
                    //StringBuilder line = new StringBuilder(paragraph.Text);
                    //for (int i = 0; i < line.Length; i++)
                    //{
                    //    var c = line[i];
                    //    if (c == ESCAPE_KEY)
                    //    {
                    //        line.Remove(i, 1);
                    //    }
                    //}
                }
            }

            Document.Replace(ESCAPE_REGEX, "");


            Document.SaveToFile(savePath);


        }

       

        public async Task<string[]> GetAllKeywordsAsync(string line)
        {
            List<string> keywords = new List<string>();
            await Task.Run(() => 
            {
                bool openedItem = false;
                bool openedKeyword = false;
                int tmpStart = 0, tmpEnd = 0;
                for (int i = 0; i < line.Length;)
                {
                    switch (line[i])
                    {
                        case ESCAPE_KEY:
                            i += 2;
                            break;
                        case START_WORD_KEY:
                        case START_LIST_KEY:
                            openedKeyword = true;
                            tmpStart = i;
                            goto default;
                        case END_WORD_KEY:
                        case END_LIST_KEY:
                            if (openedKeyword)
                            {
                                tmpEnd = i;
                                keywords.Add(line.Substring(tmpStart, tmpEnd - tmpStart + 1));
                                openedKeyword = false;
                            }
                            goto default;
                        case START_ITEM_KEY:
                            openedItem = true;
                            goto default;
                        case END_ITEM_KEY:
                            if (openedItem)
                            {
                                keywords.Add(END_ITEM_KEY.ToString());
                                openedItem = false;
                            }
                            goto default;
                        default:
                            i++;
                            break;
                    }
                }
            });
            return keywords.ToArray();
        }

        public string GetAllDocumentString(IDocument document) 
        {
            StringBuilder sb = new StringBuilder();
            foreach (Section section in document.Sections)
            {
                foreach (Paragraph paragraph in section.Paragraphs)
                {
                    sb.AppendLine(paragraph.Text);
                }
            }
            return sb.ToString();
        }

        public void CloneItemKey(string listName, List<Paragraph> paragraphs, string[] propertyNames, IEnumerable<IPropertyName> items)
        {
            IDocument document = paragraphs.First().Document;
            Section section = null;
            Paragraph insertParagraph = null;
            foreach(Section it in document.Sections)
            {
                foreach(Paragraph paragraph in it.Paragraphs)
                {
                    if (paragraph.Text.Contains(listName))
                        insertParagraph = paragraph;
                }
            }
            
            foreach (Section it in insertParagraph.Document.Sections)
            {
                if (it.Paragraphs.IndexOf(insertParagraph) != -1)
                {
                    section = it;
                    break;
                }
            }
            paragraphs.Reverse();
            for (int i = 0; i < items.ToList().Count; i++)
            {
                foreach (Paragraph paragraph in paragraphs)
                {
                    var tmpParagraph = paragraph.Clone() as Paragraph;
                    foreach(string property in propertyNames)
                    {
                        if (tmpParagraph.Text.Contains(property))
                        {
                            tmpParagraph.Replace(property, items.ToList()[i][property.Substring(1, property.Length - 2)], false, false);
                        }
                    }
                    section.Paragraphs.Insert(section.Paragraphs.IndexOf(insertParagraph) + 1, tmpParagraph);
                }
            }
        }

        public List<List<Paragraph>> GetItemKeyParagraphs(IDocument document)
        {
            List<List<Paragraph>> itemKeys = new List<List<Paragraph>>();
            List<Paragraph> itemKey = null;
            foreach (Section section in document.Sections)
            {
                var paragraphs = section.Paragraphs;
                for (int i = 0; i < section.Paragraphs.Count; i++)
                {
                    if (paragraphs[i].Text.StartsWith(START_ITEM_KEY.ToString()))
                    {
                        itemKey = new List<Paragraph>();
                        paragraphs.Remove(paragraphs[i--]);

                        continue;
                    }
                    if (paragraphs[i].Text.StartsWith(END_ITEM_KEY.ToString()))
                    {
                        itemKeys.Add(itemKey);
                        paragraphs.Remove(paragraphs[i--]);
                        itemKey = null;
                        continue;
                    }
                    if (itemKey != null)
                    {
                        itemKey.Add(section.Paragraphs[i]);
                        paragraphs.Remove(paragraphs[i--]);
                    }
                }
            }
            return itemKeys;
        }


    }
}
