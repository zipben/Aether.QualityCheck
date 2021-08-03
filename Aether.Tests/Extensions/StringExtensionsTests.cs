using Aether.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        [DataRow("abcdef", "abcdef", true)]
        [DataRow("ABCdef", "abcdef", true)]
        [DataRow("ABC", "def", false)]
        [DataRow("", "", true)]
        public void LikeTest(string str1, string str2, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, str1.Like(str2));
        }

        [TestMethod]
        [DataRow("abcdef", true)]
        [DataRow("    ", false)]
        [DataRow("", false)]
        [DataRow(null, false)]
        public void ExistsTest(string str, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, str.Exists());
        }

        [TestMethod]
        [DataRow("abcdef", "abcdef")]
        [DataRow("ThisIsACamel", "This Is A Camel")]
        [DataRow("THIIISSSSIsACamel", "THIIISSSS Is A Camel")]
        [DataRow("PartyInTheUSA", "Party In The USA")]
        [DataRow("IAmnotAProperlyConstructedSentence", "I Amnot A Properly Constructed Sentence")]
        public void SplitCameCaseTest(string str, string expectedResult)
        {
            Assert.AreEqual(expectedResult, str.SplitCamelCase());
        }

        [TestMethod]
        [DataRow("abcdef")]
        [DataRow("ThisIsACamel")]
        [DataRow("THIIISSSSIsACamel")]
        [DataRow("PartyInTheUSA")]
        [DataRow("IAmnotAProperlyConstructedSentence")]
        public void EncodeString(string str)
        {
            var encodedString = str.Encode64();

            var base64EncodedBytes = System.Convert.FromBase64String(encodedString);
            var decodedString = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            Assert.AreEqual(str, decodedString);
        }

        [TestMethod]
        [DataRow("abcdef")]
        [DataRow("ThisIsACamel")]
        [DataRow("THIIISSSSIsACamel")]
        [DataRow("PartyInTheUSA")]
        [DataRow("IAmnotAProperlyConstructedSentence")]
        public void DecodeString(string str)
        {
            var encodedString = str.Encode64();

            var decodedString = encodedString.Decode64();

            Assert.AreEqual(str, decodedString);
        }

        [TestMethod]
        [DataRow("zip@gmail.com")]
        [DataRow("burt@gurt.co.uk")]
        [DataRow("guerder@curn.co")]
        public void IsValidEmailString_AllValid(string str)
        {
            Assert.IsTrue(str.IsValidEmailAddress());
        }

        [TestMethod]
        [DataRow("zipATgmail")]
        [DataRow("burtASKJASKJAgurt")]
        [DataRow("guerder")]
        public void IsValidEmailString_AllInvalid(string str)
        {
            Assert.IsFalse(str.IsValidEmailAddress());
        }

        [TestMethod]
        [DataRow("abcdef")]
        [DataRow("ThisIsACamel")]
        [DataRow("THIIISSSSIsACamel")]
        [DataRow("PartyInTheUSA")]
        [DataRow(LORUM_IPSUM_BABY)]
        [DataRow("SAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkcSAD;lksjad;lkjsad;lkjasd;lkjasd;ljkc")]
        public void CompressDecompressString(string str)
        {
            var compressedString = str.Compress();

            var decodedString = compressedString.Decompress();

            Assert.AreEqual(str, decodedString);
        }

        private const string LORUM_IPSUM_BABY = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae mauris ornare dui euismod consectetur eu a urna. Ut iaculis tortor sit amet odio auctor, vel fringilla nibh lobortis. Mauris laoreet finibus dui, at mattis odio posuere ac. Proin mattis velit ipsum. Aenean laoreet, ante at sollicitudin elementum, quam turpis viverra orci, eget pulvinar nisi velit vitae enim. Sed volutpat tortor eu ipsum fringilla fermentum. Curabitur euismod non dolor vitae vulputate. Praesent varius ornare tortor a mollis. Maecenas ac placerat ipsum.

Praesent et dictum tortor, vel porttitor felis. Integer leo justo, tempus quis tincidunt eu, posuere ac lectus. Fusce neque nisl, tincidunt malesuada auctor at, interdum id dolor. Donec aliquam bibendum tincidunt. Nulla sed pulvinar magna. Duis eget mauris vel sapien lacinia mattis vitae eget justo. Praesent quis metus sit amet lacus posuere faucibus quis eget eros. Aenean vel bibendum nulla.

Praesent malesuada, magna faucibus elementum scelerisque, nisi dui vestibulum diam, sed volutpat nisl diam dictum augue. Pellentesque lobortis varius ligula, nec faucibus turpis porttitor non. Aliquam quis erat quis tortor efficitur suscipit. Sed id enim justo. Suspendisse id nulla risus. Phasellus sodales mauris laoreet molestie imperdiet. Integer vel massa quis dui semper maximus. Fusce eget tristique dolor. Nam convallis vestibulum neque, at venenatis ligula pellentesque tincidunt. Etiam quis feugiat ante. Phasellus nec sagittis dui, ut convallis arcu. Duis nec metus quis urna tempor eleifend.

Nam blandit lacus sed lectus sagittis gravida. Nulla lobortis enim vitae purus gravida venenatis. Mauris ornare odio eget suscipit elementum. Suspendisse in sodales diam, in consequat justo. Sed quis odio mi. Nam ex urna, tempus et velit non, sodales sollicitudin orci. Pellentesque consequat nisi nisl, sed congue arcu sodales vitae. Aenean a augue mauris. Pellentesque pellentesque justo sed venenatis fermentum. Nunc placerat mollis diam a volutpat.

Suspendisse scelerisque ultricies orci, a pharetra nulla feugiat sit amet. Mauris vel arcu accumsan, suscipit magna a, euismod quam. Sed feugiat nulla dolor, vitae finibus eros semper et. Sed non nisi iaculis, lobortis quam nec, condimentum risus. Donec euismod orci nisl, in mattis nisi facilisis in. Aliquam ac neque ornare, viverra lacus ut, aliquam ante. Integer in mauris quis ipsum posuere placerat ultricies id ante. Suspendisse augue nulla, molestie in faucibus eu, rutrum id lectus. Morbi nunc tellus, rhoncus ut justo id, egestas placerat velit. Curabitur vitae sem sollicitudin, convallis justo venenatis, cursus libero. Nam quis dolor molestie, interdum justo id, laoreet odio. Vestibulum ipsum lorem, lobortis id fringilla at, convallis id ante.

Maecenas porttitor erat nec rhoncus varius. Morbi in luctus est. Sed aliquam tempor ipsum in bibendum. Integer sit amet tellus nec tellus commodo dapibus. In pellentesque est quis nisi fringilla, nec imperdiet erat lobortis. Ut scelerisque accumsan suscipit. Vivamus rhoncus iaculis turpis, eu placerat ligula condimentum a. Cras porttitor placerat urna id tristique. Nulla eu nulla neque. Etiam a feugiat lorem. Cras pulvinar sit amet augue sed lobortis. Aenean cursus eros vitae felis ultricies, quis vehicula purus ornare. Etiam ut nisi iaculis, molestie augue nec, mattis mauris.

Vestibulum scelerisque in nulla nec convallis. Phasellus feugiat neque diam, in efficitur mi fermentum a. Integer ut laoreet sapien, vehicula feugiat sapien. Sed lacinia blandit risus, vitae accumsan odio mattis sit amet. Duis dictum urna vel lorem ultrices finibus. Nunc finibus faucibus erat, sed tempor nunc. Sed dignissim rhoncus nisl, quis volutpat tellus. Nam aliquet, dui quis laoreet sagittis, dui odio congue elit, sagittis viverra erat ipsum ut tortor. Morbi dapibus porttitor enim, in vehicula nunc dictum rhoncus. Suspendisse potenti. Donec fermentum diam at orci iaculis, quis sollicitudin tortor pretium. Morbi laoreet vestibulum odio. Aenean laoreet dolor nisl, ut sodales arcu fringilla et.

In sodales sapien urna, vitae consequat ipsum viverra et. Aliquam imperdiet aliquet tempor. Vestibulum eget tincidunt nulla. Integer elementum, arcu vitae ultrices aliquet, enim mi posuere quam, non fermentum purus enim quis metus. Etiam elementum neque in sapien tincidunt, a sagittis nisi mattis. Donec condimentum velit non nisi luctus, eget accumsan nisi semper. Praesent eget mi ipsum. Aenean a malesuada est. Etiam in erat consectetur eros interdum blandit sed eget nibh. Nunc porttitor ligula lobortis, placerat massa vel, rutrum metus. Sed quis tellus quis dui rhoncus sagittis.

Nunc pulvinar enim eu sem volutpat sagittis. Duis facilisis tincidunt est, ac semper tellus vulputate eget. Donec tellus arcu, iaculis lacinia turpis ut, maximus aliquam lacus. Praesent ornare convallis sem, eu maximus risus ullamcorper vel. Donec euismod justo nec velit dapibus, tincidunt lacinia dolor pharetra. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Phasellus auctor sem felis, vitae suscipit ante volutpat sed. Quisque pharetra nulla sed quam pulvinar ornare. Cras lectus metus, rutrum eget purus ac, posuere tincidunt ligula. Fusce molestie purus sem, at convallis turpis blandit quis. Vivamus sed mi nisl. Donec ac massa venenatis, consectetur mauris vitae, accumsan neque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Morbi ipsum massa, pulvinar accumsan diam sed, ultrices euismod tellus. Donec commodo erat diam, sed facilisis magna mattis at.

Nunc dignissim auctor elementum. Praesent bibendum euismod eros. Suspendisse eu turpis id dui laoreet dictum eu ut libero. In rutrum ex enim, vel congue diam vestibulum sit amet. Nunc non quam auctor mauris sollicitudin mollis eget eget odio. Donec a tempor lacus. Interdum et malesuada fames ac ante ipsum primis in faucibus. Donec felis tellus, sodales non sodales at, vulputate et sapien. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec in velit eu nulla mollis finibus. Suspendisse eleifend lorem id semper commodo. Aenean fermentum tellus et arcu pharetra gravida. Phasellus efficitur posuere tortor, vel sagittis turpis mollis facilisis.

Sed et sapien lacus. Praesent venenatis, lectus ut tincidunt facilisis, nisi justo luctus augue, quis faucibus sem est nec magna. Integer in euismod urna, in aliquet lorem. Aenean ultrices feugiat rhoncus. Mauris mattis elementum porttitor. Vivamus iaculis dolor sapien, eu pulvinar quam semper eget. Duis justo ligula, vehicula quis ultricies et, auctor vel ex. Nam dictum erat neque, eget aliquet velit tristique eget. Ut pellentesque eget eros eget consectetur. Morbi placerat posuere mi eu auctor. Cras odio elit, efficitur vitae vulputate sit amet, rhoncus non sapien. In sem erat, tincidunt in aliquam eget, maximus sit amet velit. Phasellus et nunc justo. Ut suscipit eros id lectus accumsan, nec aliquam arcu aliquet. Donec rhoncus dui eu ullamcorper ullamcorper.

Nunc lacinia odio sed egestas feugiat. Duis sollicitudin purus at aliquet pellentesque. Nulla faucibus ullamcorper ante quis aliquet. Proin at neque quam. Vestibulum in tellus eget est egestas rhoncus id a enim. Aliquam eget facilisis odio. Suspendisse sed aliquam orci. Aenean aliquet orci mauris, ac pharetra massa molestie in. Vestibulum hendrerit risus sit amet placerat venenatis. Nunc quis risus in nisl mattis egestas. Interdum et malesuada fames ac ante ipsum primis in faucibus. Pellentesque nunc sem, congue sit amet nisl quis, luctus interdum ipsum. Suspendisse potenti.

Fusce et pharetra ex. Aenean suscipit consectetur accumsan. Fusce purus mauris, fringilla eget ante eget, dignissim lacinia augue. Nullam tristique lacus tincidunt egestas accumsan. Nullam vel feugiat justo. Nulla finibus dignissim arcu, sed consectetur neque tempus quis. Nam at orci vel arcu fermentum venenatis quis nec lorem. Nam efficitur vel nisi vitae commodo. Quisque lacus lacus, euismod eget venenatis quis, mattis non mi. Curabitur malesuada quam a quam mollis, quis tristique nisl elementum. Quisque id ante tincidunt, fringilla ex et, consequat sapien.

Aenean fringilla facilisis blandit. Donec varius nulla a sem placerat luctus. Mauris iaculis eget dolor pharetra pellentesque. Fusce at feugiat risus, aliquet lobortis augue. Quisque quis elit pharetra, auctor elit quis, convallis diam. Phasellus ornare dictum turpis, eget pretium purus auctor a. Integer imperdiet eget nibh sit amet faucibus. Nullam justo sapien, egestas vel interdum et, tincidunt eleifend ante. Donec pretium risus sed diam blandit, eu varius turpis auctor.

Proin quis elementum ligula. Aenean erat diam, vestibulum vitae porta ac, fermentum id magna. Aenean efficitur ex non velit ullamcorper, vitae hendrerit erat rhoncus. Curabitur metus magna, consequat at orci ac, luctus porta leo. Integer quis eleifend turpis, non laoreet justo. In et massa vitae nisi mollis eleifend. Quisque at ipsum eu nunc facilisis imperdiet.

Sed tempor eros ut porttitor ultrices. Vestibulum tristique eleifend dolor, ut aliquet ligula dapibus feugiat. Cras et urna aliquet, euismod nisi quis, fringilla justo. Curabitur pellentesque sapien in congue consectetur. Duis eget fringilla ligula. Nullam in eros at lectus feugiat accumsan sed quis metus. Sed mollis augue faucibus neque pharetra dictum. In mollis orci vel fermentum aliquam. Donec vehicula, justo eu pulvinar maximus, urna turpis egestas lectus, at iaculis justo turpis quis ipsum. Etiam imperdiet orci malesuada rutrum tempor. Nullam tristique ex nisi, a sagittis urna rhoncus non. Vivamus tincidunt blandit massa, ac commodo lorem aliquam eu. Vestibulum auctor est non finibus vestibulum. Sed leo ante, semper sed fringilla ac, lobortis ut nisi. Ut non condimentum lorem, id condimentum sapien. Integer ante felis, finibus at tortor vel, tempus rhoncus metus. Proin auctor ac est eget tristique. Donec auctor nulla eros, quis tincidunt turpis tempor ac vel.";
    }
}
