// THIS IS A GENERATED FILE, DO NOT EDIT!
// generate it by saving the file '../_ContentGenerate.tt'. It should be located next to the *.csproj file.

using BytingLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace BC7
{
    public class ContentLoader
    {
        public _Fonts Fonts { get; }
        public _Sounds Sounds { get; }
        public _Textures Textures { get; }
        protected readonly IContentCollector collector;
        protected readonly DisposableContainer disposables;
        protected readonly string basePath;
        public ContentLoader(IContentCollector collector, DisposableContainer disposables)
        {
            this.collector = collector;
            this.disposables = disposables;
            this.basePath = "";
            Fonts = new _Fonts(collector, disposables);
            Sounds = new _Sounds(collector, disposables);
            Textures = new _Textures(collector, disposables);
        }
        public Ref<T> Use<T>(string assetNameWithoutDirectory)
        {
            return disposables.Use(collector.Use<T>(basePath + assetNameWithoutDirectory));
        }
        public class _Fonts
        {
            protected readonly IContentCollector collector;
            protected readonly DisposableContainer disposables;
            protected readonly string basePath;
            public _Fonts(IContentCollector collector, DisposableContainer disposables)
            {
                this.collector = collector;
                this.disposables = disposables;
                this.basePath = "Fonts/";
                MessageBoxFont = disposables.Use(collector.Use<SpriteFont>(basePath + "MessageBox"));
                TahomaFont = disposables.Use(collector.Use<SpriteFont>(basePath + "Tahoma"));
                Tahoma_boldFont = disposables.Use(collector.Use<SpriteFont>(basePath + "Tahoma.bold"));
                TinyFont = disposables.Use(collector.Use<SpriteFont>(basePath + "Tiny"));
            }
            public Ref<T> Use<T>(string assetNameWithoutDirectory)
            {
                return disposables.Use(collector.Use<T>(basePath + assetNameWithoutDirectory));
            }
            public Ref<SpriteFont> MessageBoxFont { get; }
            public Ref<SpriteFont> TahomaFont { get; }
            public Ref<SpriteFont> Tahoma_boldFont { get; }
            public Ref<SpriteFont> TinyFont { get; }
        }
        public class _Sounds
        {
            protected readonly IContentCollector collector;
            protected readonly DisposableContainer disposables;
            protected readonly string basePath;
            public _Sounds(IContentCollector collector, DisposableContainer disposables)
            {
                this.collector = collector;
                this.disposables = disposables;
                this.basePath = "Sounds/";
                kickCar1Sfx = disposables.Use(collector.Use<SoundEffect>(basePath + "kickCar1"));
            }
            public Ref<T> Use<T>(string assetNameWithoutDirectory)
            {
                return disposables.Use(collector.Use<T>(basePath + assetNameWithoutDirectory));
            }
            public Ref<SoundEffect> kickCar1Sfx { get; }
        }
        public class _Textures
        {
            protected readonly IContentCollector collector;
            protected readonly DisposableContainer disposables;
            protected readonly string basePath;
            public _Textures(IContentCollector collector, DisposableContainer disposables)
            {
                this.collector = collector;
                this.disposables = disposables;
                this.basePath = "Textures/";
                car1ColorTex = disposables.Use(collector.Use<Texture2D>(basePath + "car1Color"));
            }
            public Ref<T> Use<T>(string assetNameWithoutDirectory)
            {
                return disposables.Use(collector.Use<T>(basePath + assetNameWithoutDirectory));
            }
            public Ref<Texture2D> car1ColorTex { get; }
        }
    }
}
