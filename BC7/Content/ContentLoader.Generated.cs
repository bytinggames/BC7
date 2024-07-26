// THIS IS A GENERATED FILE, DO NOT EDIT!
// generate it by saving the file '../_ContentGenerate.tt'. It should be located next to the *.csproj file.

using BytingLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace BC7
{
    public class ContentLoader
    {
        public _Effects Effects { get; }
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
            Effects = new _Effects(collector, disposables);
            Fonts = new _Fonts(collector, disposables);
            Sounds = new _Sounds(collector, disposables);
            Textures = new _Textures(collector, disposables);
        }
        public Ref<T> Use<T>(string assetNameWithoutDirectory)
        {
            return disposables.Use(collector.Use<T>(basePath + assetNameWithoutDirectory));
        }
        public class _Effects
        {
            public _D2 D2 { get; }
            protected readonly IContentCollector collector;
            protected readonly DisposableContainer disposables;
            protected readonly string basePath;
            public _Effects(IContentCollector collector, DisposableContainer disposables)
            {
                this.collector = collector;
                this.disposables = disposables;
                this.basePath = "Effects/";
                D2 = new _D2(collector, disposables);
            }
            public Ref<T> Use<T>(string assetNameWithoutDirectory)
            {
                return disposables.Use(collector.Use<T>(basePath + assetNameWithoutDirectory));
            }
            public class _D2
            {
                protected readonly IContentCollector collector;
                protected readonly DisposableContainer disposables;
                protected readonly string basePath;
                public _D2(IContentCollector collector, DisposableContainer disposables)
                {
                    this.collector = collector;
                    this.disposables = disposables;
                    this.basePath = "Effects/D2/";
                    HueShiftFx = disposables.Use(collector.Use<Effect>(basePath + "HueShift"));
                }
                public Ref<T> Use<T>(string assetNameWithoutDirectory)
                {
                    return disposables.Use(collector.Use<T>(basePath + assetNameWithoutDirectory));
                }
                public Ref<Effect> HueShiftFx { get; }
            }
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
                BackTex = disposables.Use(collector.Use<Texture2D>(basePath + "Back"));
                CloverTex = disposables.Use(collector.Use<Texture2D>(basePath + "Clover"));
                FlowerTex = disposables.Use(collector.Use<Texture2D>(basePath + "Flower"));
                Mat_0Tex = disposables.Use(collector.Use<Texture2D>(basePath + "Mat_0"));
                Mat_1Tex = disposables.Use(collector.Use<Texture2D>(basePath + "Mat_1"));
                SkullTex = disposables.Use(collector.Use<Texture2D>(basePath + "Skull"));
            }
            public Ref<T> Use<T>(string assetNameWithoutDirectory)
            {
                return disposables.Use(collector.Use<T>(basePath + assetNameWithoutDirectory));
            }
            public Ref<Texture2D> BackTex { get; }
            public Ref<Texture2D> CloverTex { get; }
            public Ref<Texture2D> FlowerTex { get; }
            public Ref<Texture2D> Mat_0Tex { get; }
            public Ref<Texture2D> Mat_1Tex { get; }
            public Ref<Texture2D> SkullTex { get; }
        }
    }
}
