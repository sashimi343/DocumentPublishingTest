namespace Sample.Core;

/// <summary>
///     <para xml:lang="en">Creates <see cref="Greeting"/> instances.</para>
///     <para xml:lang="ja"><see cref="Greeting"/>を生成する。</para>
/// </summary>
public class Greeter
{
    /// <summary>
    ///     <para xml:lang="en">Creates a greeting for the specified name.</para>
    ///     <para xml:lang="ja">指定した名前に対する挨拶を生成する。</para>
    /// </summary>
    /// <param name="name">
    ///     <para xml:lang="en">The name of the person to greet.</para>
    ///     <para xml:lang="ja">挨拶する相手の名前。</para>
    /// </param>
    /// <returns>
    ///     <para xml:lang="en">The created <see cref="Greeting"/>.</para>
    ///     <para xml:lang="ja">生成した<see cref="Greeting"/>。</para>
    /// </returns>
    /// <seealso cref="Greeting.Text"/>
    public virtual Greeting Greet(string name) => new($"Hello, {name}!");
}
