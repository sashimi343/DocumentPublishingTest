using Sample.Core;

namespace Sample.Extensions;

/// <summary>
///     <para xml:lang="en">A <see cref="Greeter"/> that shouts. Overrides <see cref="Greeter.Greet(string)"/>.</para>
///     <para xml:lang="ja">大声で挨拶する<see cref="Greeter"/>。<see cref="Greeter.Greet(string)"/>をオーバーライドする。</para>
/// </summary>
/// <seealso cref="Greeting"/>
/// <seealso cref="GreetingExtensions"/>
public class LoudGreeter : Greeter
{
    /// <inheritdoc/>
    public override Greeting Greet(string name) => base.Greet(name).ToLoud();
}
